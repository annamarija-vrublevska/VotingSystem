using VotingSystem.Api.Entities;

namespace VotingSystem.Api.Logic
{
    public interface IVoterRepository
    {
        Task<IEnumerable<Voter>> GetAllVotersAsync();
        Task<Voter?> GetVoterByUserNameAsync(string userName);
        Task AddVoterAsync(Voter voter);
        void DeleteVoter(Voter voter);
        Task<bool> VoterExists(string userName);
        Task SaveChangesAsync();
    }
}
