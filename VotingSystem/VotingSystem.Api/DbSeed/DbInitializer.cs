using VotingSystem.Api.DbContexts;
using VotingSystem.Api.Entities;

namespace VotingSystem.Api.DbSeed
{
    public class DbInitializer
    {
        public static void Initialize(VotingSystemContext context)
        {
            context.Database.EnsureCreated();

            if (context.Items.Any())
            {
                context.Items.RemoveRange(context.Items);
                context.SaveChanges();
            }

            if (context.Voters.Any())
            {
                context.Voters.RemoveRange(context.Voters);
                context.SaveChanges();
            }

            var voters = new Voter[]
            {
                new() { UserName = "UserName1", Name = "Name1", Email = "email1@email.com" },
                new() { UserName = "UserName2", Name = "Name2", Email = "email2@email.com" },
                new() { UserName = "UserName3", Name = "Name3", Email = "email3@email.com" },
                new() { UserName = "UserName4", Name = "Name4", Email = "email4@email.com" }
            };

            foreach (var voter in voters)
            {
                context.Voters.Add(voter);
            }

            context.SaveChanges();

            var items = new Item[]
            {
                new() {Name = "ItemName1", Volume = 0, UserName = "UserName1", ExpireDate = DateTime.Now.AddDays(2)},
                new() {Name = "ItemName2", Volume = 0, UserName = "UserName2", ExpireDate = DateTime.Now.AddDays(2)},
                new() {Name = "ItemName3", Volume = 0, UserName = "UserName3", ExpireDate = DateTime.Now.AddDays(2)},
                new() {Name = "ItemName4", Volume = 0, UserName = "UserName4", ExpireDate = DateTime.Now.AddDays(2)}
            };

            foreach (var item in items)
            {
                context.Items.Add(item);
            }

            context.SaveChanges();
        }
    }
}
