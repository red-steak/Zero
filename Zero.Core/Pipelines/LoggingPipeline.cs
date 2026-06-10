using System.Diagnostics;

namespace Zero.Core.Pipelines;

public class LoggingPipeline : ICommandPipeline
{
    public Task<bool> BeforeExecuteAsync(CommandContext ctx)
    {
        Debug.WriteLine($"[CMD] {ctx.Kind} starting on {ctx.Holder.GetType().Name}");
        return Task.FromResult(true);
    }

    public Task AfterExecuteAsync(CommandContext ctx)
    {
        Debug.WriteLine($"[CMD] {ctx.Kind} finished");
        return Task.CompletedTask;
    }

    public Task OnErrorAsync(CommandContext ctx, Exception ex)
    {
        Debug.WriteLine($"[CMD] {ctx.Kind} error: {ex}");
        return Task.CompletedTask;
    }
}
