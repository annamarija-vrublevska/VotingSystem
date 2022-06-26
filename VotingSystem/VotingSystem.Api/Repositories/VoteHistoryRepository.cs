using VotingSystem.Api.DbContexts;
using VotingSystem.Api.Entities;
using VotingSystem.Api.Repositories.Interfaces;

namespace VotingSystem.Api.Repositories
{
    public class VoteHistoryRepository : IVoteHistoryRepository
    {
        private readonly VotingSystemContext _context;

        public VoteHistoryRepository(VotingSystemContext context)
        {
            _context = context;
        }
        public async Task UpdateItemVoteHistory(string userName, long itemId)
        {
            await _context.VoteHistory.AddAsync(new VoteHistory
            {
                UserName = userName,
                ItemId = itemId
            });

            await _context.SaveChangesAsync();
        }

        public bool HasAlreadyVoted(string userName, long itemId)
        {
            return _context.VoteHistory
                .Any(o => o.UserName == userName && o.ItemId == itemId);
        }
    }
}
