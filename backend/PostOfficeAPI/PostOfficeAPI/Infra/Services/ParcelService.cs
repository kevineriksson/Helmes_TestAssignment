using Microsoft.AspNetCore.Mvc;
using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Contracts.Services;

namespace PostOfficeAPI.Infra.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelRepo _parcelRepo;
        public ParcelService(IParcelRepo parcelRepo)
        {
            _parcelRepo = parcelRepo;
        }
        public async Task<IEnumerable<Parcel>> GetAllParcelsAsync() => await _parcelRepo.GetAllParcelsAsync();
        public async Task<Parcel> GetParcelByBagIdAsync(string Id) => await _parcelRepo.GetParcelByIdAsync(Id);
        public async Task<List<Parcel>> GetParcelsByBagIdAsync(string Id) => await _parcelRepo.GetParcelsByBagIdAsync(Id);
        public async Task<Parcel> CreateParcelAsync(ParcelDto parcelDto) => await _parcelRepo.CreateParcelAsync(parcelDto);
        public async Task<bool> DeleteParcelAsync(string Id)
        {
            var parcel = await _parcelRepo.GetParcelByIdAsync(Id);
            if (parcel != null)
            {
                var deletedParcel = await _parcelRepo.DeleteAsync(parcel.Id);
                return deletedParcel;
            }
            return false;
        }
        public async Task<bool> UpdateParcelAsync(Parcel parcel)
        {
            parcel = await _parcelRepo.GetParcelByIdAsync(parcel.Id);

            if (parcel == null)
            {
                throw new ArgumentException("Parcel not found");
            }
            var success = await _parcelRepo.UpdateAsync(parcel.Id, parcel);
            if (!success)
            {
                throw new InvalidOperationException("Failed to update the parcel");
            }
            return success;
        }
    }
}
