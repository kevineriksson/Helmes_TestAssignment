namespace PostOfficeAPI.Contracts.Repos
{
    public interface IParcelRepo : IRepo<Parcel>
    {
        Task<IEnumerable<Parcel>> GetAllParcelsAsync();
        Task<Parcel> GetParcelByIdAsync(string id);
        Task<List<Parcel>> GetParcelsByDestinationAsync(string destination);
        Task<List<Parcel>> GetParcelsByBagIdAsync(string bagId);
        Task<Parcel> CreateParcelAsync(ParcelDto parcelDto);
    }
}
