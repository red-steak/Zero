using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace Zero.Core.Workspaces
{
    public partial class Message : ReactiveObject
    {

        public Message(IReactiveCommand command, Guid currentId)
        {
            Id = currentId;
            Command = command;
            Start = DateTime.Now;
            ClipboardText = ToString();
            //this.WhenAnyValue(x => Kind, x => Text, x => Start, x => End, x => Duration).Subscribe((x) => { ClipboardText = $"{x.Item1}\t{x.Item2}\t{x.Item3}\t{x.Item4}\t{x.Item5}"; });
            this.WhenAnyValue(x => x.Kind).Subscribe(kind => { if (kind == MessageKind.Success) { End = DateTime.Now; Duration = TimeSpan.FromTicks(End.Value.Ticks - Start.Value.Ticks); } });
        }

        public Guid Id { get; init; }

        public IReactiveCommand Command { get; init; }
        [Reactive] MessageKind kind;
        [Reactive] string? text;
        [Reactive] DateTime? start;
        [Reactive] DateTime? end;
        [Reactive] TimeSpan? duration;

        // Generates the IsOpen property
        [Reactive] bool isOpen;

        [Reactive] string? clipboardText;

        // Generates the SetOpenCommand
        [ReactiveCommand]
        private void SetOpen(bool value)
        {
            IsOpen = value;
        }

        // Generates the CloseCommand
        [ReactiveCommand]
        private void Close()
        {
            IsOpen = false;
        }

        // Generates the ToggleOpenCommand
        [ReactiveCommand]
        private void ToggleOpen()
        {
            IsOpen = !IsOpen;
        }

        // Generates the CopyCommand
        [ReactiveCommand]
        private void Copy()
        {
            //await clipboard.SetDataAsync(data);
        }


        public override string ToString()
        {
            return $"{Text}\t{Start}\t{End}\t{Duration}\t{Kind}";
        }
    }
}