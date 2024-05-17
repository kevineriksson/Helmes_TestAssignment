using PostOfficeAPI.Models;

namespace PostOfficeAPI.Contracts.Repos
{
    public interface IBagRepo : IRepo<Bag>
    {
        Task<Bag> GetBagByIdAsync(string id);
        Task<IEnumerable<Bag>> GetAllBagsAsync();
        Task<List<Bag>> GetBagsByShipmentId(string shipmentId);
        Task<List<BagWithParcels>> GetBagsWithParcels(Parcel parcel);
        Task<List<BagWithLetters>> GetBagsWithLetters();
    }
}
