using Microsoft.EntityFrameworkCore;
using VotingSystem.Api.DbContexts;
using VotingSystem.Api.Entities;
using VotingSystem.Api.Repositories.Interfaces;

namespace VotingSystem.Api.Repositories
{
    public class VoterRepository : IVoterRepository
    {
        private readonly VotingSystemContext _context;

        public VoterRepository(VotingSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voter>> GetAllVotersAsync()
        {
            return await _context.Voters.OrderBy(o => o.UserName).ToListAsync();
        }

        public async Task<Voter?> GetVoterByUserNameAsync(string userName)
        {
            return await _context.Voters.FirstOrDefaultAsync(o => o.UserName == userName);
        }

        public async Task AddVoterAsync(Voter voter)
        {
            await _context.Voters.AddAsync(voter);
        }

        public void DeleteVoter(Voter voter)
        {
            _context.Voters.Remove(voter);
        }

        public async Task<bool> VoterExists(string userName)
        {
            return await _context.Voters.AnyAsync(o => o.UserName == userName);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}