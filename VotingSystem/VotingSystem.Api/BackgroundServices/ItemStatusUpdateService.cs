using VotingSystem.Api.DbContexts;
using VotingSystem.Api.Enums;

namespace VotingSystem.Api.BackgroundServices
{
    public class ItemStatusUpdateService : IItemStatusUpdateService
    {
        private readonly VotingSystemContext _context;

        public ItemStatusUpdateService(VotingSystemContext context)
        {
            _context = context;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var item in _context.Items.Where(o => o.ExpireDate < DateTime.Now && o.State == StateType.New))
                {
                    if (item.Volume == 0)
                    {
                        item.State = StateType.Obsolete;
                        continue;
                    }

                    if (item.Volume / (_context.Voters.Count() - 1) * 100 > 51)
                    {
                        item.State = StateType.Approved;
                    }
                    else
                    {
                        item.State = StateType.Rejected;
                    }
                }
                await _context.SaveChangesAsync();
                await Task.Delay(10000);
            }
        }
    }
}
