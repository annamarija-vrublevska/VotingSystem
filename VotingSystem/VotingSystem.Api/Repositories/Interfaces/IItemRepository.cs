using VotingSystem.Api.Entities;

namespace VotingSystem.Api.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item?> GetItemByIdAsync(long id);
        Task AddItemAsync(Item item);
        void DeleteItem(Item item);
        Task SaveChangesAsync();
    }
}
