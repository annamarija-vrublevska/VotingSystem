namespace VotingSystem.Api.BackgroundServices
{
    public interface IItemStatusUpdateService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
