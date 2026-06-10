using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.ComponentModel.Design;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks.Sources;
using System.Windows.Input;
using Zero.Core.KeyboardShortcuts;
using Zero.Core.Pipelines;
using Zero.Core.Ribbon;

namespace Zero.Core.Commands
{
    public abstract partial class BaseCommandsHolder : ReactiveObject
    {
        bool _isInitialized = false;


        private readonly IEnumerable<ICommandPipeline> _pipelines;
        private readonly Dictionary<Guid, CommandKind> _kinds = [];
        private readonly Dictionary<Guid, CommandKind[]> _forbidden = [];
        private readonly Dictionary<Guid, RibbonAttribute> _ribbon = [];
        private readonly Dictionary<Guid, KeyboardShortcutAttribute> _shortcuts = [];

        [Reactive] IObservable<HashSet<CommandKind>>? activeKinds;
        [Reactive] bool isBusy = false;

        public List<CommandPropertyPair> CommandPropertyPairs { get; } = [];

        public BaseCommandsHolder(IEnumerable<ICommandPipeline> pipelines)
        {
            _pipelines = pipelines;
        }

        public void InitializeCommands()
        {
            if (_isInitialized) return;

            var methods = GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(m => m.IsPrivate && m.DeclaringType!.BaseType
                == typeof(BaseCommandsHolder));

            var properties = GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.DeclaringType!.BaseType == typeof(BaseCommandsHolder));            

            foreach (var m in methods) 
            {
                var methodName = m.Name;
                var propertyName = $"{m.Name}Command";
                var property = properties.FirstOrDefault(p => p.Name == propertyName);
                if (property is not null)
                {
                    CommandPropertyPairs.Add(new(m, property));
                }
            }

            ActiveKinds = CommandPropertyPairs
            .Select(pair =>
            {
                var cmd = pair.P.GetValue(this);

                IObservable<bool> isExec = cmd switch
                {
                    ReactiveCommand<Unit, Unit> c => c.IsExecuting,
                    ReactiveCommand<Unit, object> c => c.IsExecuting,
                    ReactiveCommand<object, Unit> c => c.IsExecuting,
                    ReactiveCommand<object, object> c => c.IsExecuting,
                    _ => Observable.Return(false)
                };

                var kind = pair.P.GetCustomAttribute<CommandAttribute>()?.Kind ?? default;

                return isExec.Select(exec => (kind, exec));
                })
            .Merge()
            .Scan(new HashSet<CommandKind>(), (set, update) =>
            {
                if (update.exec) 
                    set.Add(update.kind);
                else 
                    set.Remove(update.kind);
                return [.. set];
            })
            .Replay(1)
            .RefCount();

            ActiveKinds
                .Select(k => k.Any())
                .ToProperty(this, x => x.IsBusy);

            // 🔹 TADY: naplnit metadata pro všechny původní commandy
            foreach (var m in methods)
            {
                var kindAttr = m.GetCustomAttribute<CommandAttribute>();
                if (kindAttr is null) continue;

                var guid = Guid.Parse(kindAttr.Id);

                _kinds[guid] = kindAttr.Kind;

                var forbAttr = m.GetCustomAttribute<NoExecCommandsAttribute>();
                if (forbAttr is not null)
                    _forbidden[guid] = forbAttr.ForbiddenKinds;

                var ribAttr = m.GetCustomAttribute<RibbonAttribute>();
                if (ribAttr is not null)
                    _ribbon[guid] = ribAttr;

                var scAttr = m.GetCustomAttribute<KeyboardShortcutAttribute>();
                if (scAttr is not null)
                    _shortcuts[guid] = scAttr;
            }

            //// 🔹 Až potom wrapování
            //foreach (var (m, original, kind, forbidden) in
            //         from m in methods
            //         let original = m
            //         let kind = m.GetCustomAttribute<CommandAttribute>()?.Kind ?? default
            //         let forbidden = m.GetCustomAttribute<NoExecCommandsAttribute>()?.ForbiddenKinds ?? []
            //         select (m, original, kind, forbidden))
            //{
            //    if (original == null) continue;

            //    var type = commandPropertyPairs.FirstOrDefault(x => x.M == m).GetType();
            //    var args = type.GetGenericArguments(); // [TIn, TOut]

            //    var method = typeof(BaseCommandsHolder)
            //        .GetMethod(nameof(WrapCommand), BindingFlags.Instance | BindingFlags.NonPublic)!
            //        .MakeGenericMethod(args[0], args[1]);

            //    var wrapped = method.Invoke(this,
            //    [
            //        original,
            //        kind,
            //        forbidden ?? [kind]
            //    ]);

            //    m.SetValue(this, wrapped);
            //}
        
            _isInitialized = true;
        }


       private ReactiveCommand<TIn, TOut> WrapCommand<TIn, TOut>(Guid id,
            ReactiveCommand<TIn, TOut> original,
            CommandKind kind,
            CommandKind[] forbidden)
        {
            // 1) Naše vlastní CanExecute logika
            var customCanExecute = ActiveKinds?.Select(active =>
            {
                if (active.Any())
                    return true;

                if (forbidden.Any(f => active.Any(a => a == f)))
                    return false;

                if (_forbidden.Any(kv => kv.Value.Contains(kind)))
                    return false;

                return true;
            });

            var combinedCanExecute =
                original.CanExecute
                        .CombineLatest(customCanExecute, (orig, custom) => orig && custom);
            
            // 3) Wrapper command
            var wrapped = ReactiveCommand.CreateFromTask<TIn, TOut>(
                async param =>
                {
                    var ctx = new CommandContext(this, original, kind, param);

                    // BEFORE
                    foreach (var pipe in _pipelines)
                        if (!await pipe.BeforeExecuteAsync(ctx))
                            return default!;

                    try
                    {
                        // původní command        
                        var result = await original.Execute(param).ToTask();

                        // AFTER
                        foreach (var pipe in _pipelines)
                            await pipe.AfterExecuteAsync(ctx);

                        return result;
                    }
                    catch (Exception ex)
                    {
                        // ERROR
                        foreach (var pipe in _pipelines)
                            await pipe.OnErrorAsync(ctx, ex);

                        throw;
                    }
                },
                combinedCanExecute);

            // 4) Přeneseme metadata (Ribbon, Shortcut, Forbidden, Kind)
            if (_kinds.TryGetValue(id, out var k))
            {
                _kinds.Remove(id);
                _kinds[id] = k;
            }

            if (_forbidden.TryGetValue(id, out var forb))
            {
                _forbidden.Remove(id);
                _forbidden[id] = forb;
            }

            if (_ribbon.TryGetValue(id, out var rib))
            {
                _ribbon.Remove(id);
                _ribbon[id] = rib;
            }

            if (_shortcuts.TryGetValue(id, out var sc))
            {
                _shortcuts.Remove(id);
                _shortcuts[id] = sc;
            }

            return wrapped;
        }


        public IEnumerable<(IReactiveCommand Command, RibbonAttribute Ribbon)> GetRibbonCommands(List<CommandPropertyPair> values)
        {
            var dictionary = _ribbon.Select(kv => (kv.Key, kv.Value));
            var methods = values.Select(v => v.M);
            var result = new Dictionary<IReactiveCommand, RibbonAttribute>();
            foreach ((Guid Key, RibbonAttribute Value) d in dictionary)
            {
                foreach (var m in methods)
                {
                    var att = m.GetCustomAttribute<CommandAttribute>();
                    if (att is not null) 
                    {
                        var attId = Guid.Parse(att.Id);
                        var methodId = d.Key;
                        if (attId.Equals(methodId))
                        {
                            var name = m.Name;
                            var propertyName = $"{name}Command";
                            var command = (IReactiveCommand)values.FirstOrDefault(v => v.P.Name == propertyName)!.P.GetValue(this)!;
                            result.Add(command, d.Value);
                        }
                    }
                }
            }

            var rrr = result.Select(r => (r.Key, r.Value));
            return rrr;
        }

        public IEnumerable<(Guid, KeyboardShortcutAttribute Shortcut)> GetShortcuts()
            => _shortcuts.Select(kv => (kv.Key, kv.Value));
    }
}
