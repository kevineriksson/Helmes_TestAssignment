using PostOfficeAPI.ApplicationCore.Models;

namespace PostOfficeAPI.ApplicationCore.Contracts.Repos
{
    public interface IBagRepo : IRepo<Bag>
    {
        Task<Bag> GetBagByIdAsync(string id);
        Task<IEnumerable<Bag>> GetAllBagsAsync();
        Task<List<Bag>> GetBagsByShipmentIdAsync(string shipmentId);
        Task<List<BagWithParcels>> GetBagsWithParcelsAsync(Parcel parcel);
        Task<List<BagWithLetters>> GetBagsWithLettersAsync();
    }
}
