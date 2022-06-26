namespace VotingSystem.Api.Repositories.Interfaces
{
    public interface IVoteHistoryRepository
    {
        Task UpdateItemVoteHistory(string userName, long itemId);
        bool HasAlreadyVoted(string userName, long itemId);
    }
}
