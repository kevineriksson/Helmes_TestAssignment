namespace PostOfficeAPI.ApplicationCore.Contracts.Repos
{
    public interface IParcelRepo : IRepo<Parcel>
    {
        Task<Parcel> GetParcelByIdAsync(string id);
        Task<IEnumerable<Parcel>> GetAllParcelsAsync();
        Task<List<Parcel>> GetParcelsByBagIdAsync(string bagId);
        Task<Parcel> CreateParcelAsync(ParcelDto parcelDto);
    }
}
