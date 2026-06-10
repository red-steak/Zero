namespace Zero.Core.Pipelines
{
    public interface ICommandPipeline
    {
        Task<bool> BeforeExecuteAsync(CommandContext ctx);
        Task AfterExecuteAsync(CommandContext ctx);
        Task OnErrorAsync(CommandContext ctx, Exception ex);
    }
}
