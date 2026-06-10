namespace Zero.Core.Pipelines;

public class ErrorPipeline : ICommandPipeline
{
    public Task<bool> BeforeExecuteAsync(CommandContext ctx) => Task.FromResult(true);

    public Task AfterExecuteAsync(CommandContext ctx) => Task.CompletedTask;

    public Task OnErrorAsync(CommandContext ctx, Exception ex)
    {
        // sem si pak dáš dialog / logger / telemetry
        return Task.CompletedTask;
    }
}