using Microsoft.AspNetCore.Mvc;
using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Contracts.Services;



namespace PostOfficeAPI.Infra.Services
{
    public class ParcelService : IParcelService
    {
        private readonly IParcelRepo _parcelRepo;
        private readonly IBagRepo _bagRepo;
        public ParcelService(IParcelRepo parcelRepo, IBagRepo bagRepo)
        {
            _parcelRepo = parcelRepo;
            _bagRepo = bagRepo;
        }
        public async Task<Parcel> CreateParcelAsync(ParcelDto parcelDto)
        {
            return await _parcelRepo.CreateParcelAsync(parcelDto);
        }
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
        public async Task<IEnumerable<Parcel>> GetAllParcelsAsync()
        {
            return await _parcelRepo.GetAllParcelsAsync();
        }
        public async Task<Parcel> GetParcelByBagIdAsync(string Id)
        {
            return await _parcelRepo.GetParcelByIdAsync(Id);
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
        public async Task<List<Parcel>> GetParcelsByBagIdAsync(string Id)
        {
            return await _parcelRepo.GetParcelsByBagIdAsync(Id);
        }
    }
}
