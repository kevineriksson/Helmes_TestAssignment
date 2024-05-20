namespace PostOfficeAPI.ApplicationCore.Contracts.Services
{
    public interface IParcelService
    {
        Task<IEnumerable<Parcel>> GetAllParcelsAsync();
        Task<List<Parcel>> GetParcelsByBagIdAsync(string Id);
        Task<Parcel> GetParcelByBagIdAsync(string Id);
        Task<Parcel> CreateParcelAsync(ParcelDto parcelDto);
        Task<bool> UpdateParcelAsync(Parcel parcel);
        Task<bool> DeleteParcelAsync(string Id);
    }

}
