using PostOfficeAPI.Models;

namespace PostOfficeAPI.Contracts.Services
{
    public interface IBagService
    {
        Task<IEnumerable<Bag>> GetAllBagsAsync();
        Task<List<Bag>> GetBagsByShimpentId(string Id);
        Task<Bag> GetBagByIdAsync(string Id);
        Task<Bag> CreateBagAsync(Bag bag);
        Task<bool> UpdateBagAsync(Bag bag);
        Task<bool> DeleteBagAsync(string Id);
    }
}
