using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VotingSystem.Api.Controllers;
using VotingSystem.Api.DbContexts;
using VotingSystem.Api.Entities;
using VotingSystem.Api.Repositories.Interfaces;

namespace VotingSystem.Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly VotingSystemContext _context;

        public ItemRepository(VotingSystemContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            return await _context.Items
                .Include(o => o.Owner)
                .OrderBy(o => o.ExpireDate)
                .ToListAsync();
        }

        public async Task<Item?> GetItemByIdAsync(long id)
        {
            return await _context.Items
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
        }

        public void DeleteItem(Item item)
        {
            _context.Items.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
