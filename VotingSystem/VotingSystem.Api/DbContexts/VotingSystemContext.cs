using Microsoft.EntityFrameworkCore;
using VotingSystem.Api.Entities;

namespace VotingSystem.Api.DbContexts
{
    public class VotingSystemContext : DbContext
    {
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Item> Items { get; set; }

        public VotingSystemContext(DbContextOptions<VotingSystemContext> options)
            : base(options)
        {

        }
    }
}
