using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Contracts.Services;
using PostOfficeAPI.ApplicationCore.Models;

namespace PostOfficeAPI.Infra.Services
{
    public class BagService : IBagService
    {
        private readonly IBagRepo _bagRepo;
        public BagService(IBagRepo bagRepo)
        {
            _bagRepo = bagRepo;
        }
        public async Task<IEnumerable<Bag>> GetAllBagsAsync() => await _bagRepo.GetAllBagsAsync();
        public async Task<Bag> GetBagByIdAsync(string Id) => await _bagRepo.GetBagByIdAsync(Id);
        public async Task<List<Bag>> GetBagsByShimpentIdAsync(string Id) => await _bagRepo.GetBagsByShipmentIdAsync(Id);
        public async Task<Bag> CreateBagAsync(Bag bag)
        {
            if (bag is BagWithParcels && ((BagWithParcels)bag).Parcels.Any())
            {
                throw new ArgumentException("New parcel bags should not contain parcels initially");
            }
            else if (bag is BagWithLetters lettersBag && lettersBag.CountOfLetters < 1)
            {
                throw new ArgumentException("Letter bags must have at least one letter.");
            }
            return await _bagRepo.CreateAsync(bag);
        }
        public async Task<bool> UpdateBagAsync(Bag updatedBag)
        {
            var existingBag = await _bagRepo.GetBagByIdAsync(updatedBag.Id);

            if (existingBag == null)
            {
                throw new ArgumentException("Bag not found");
            }

            /*if (existingBag.IsFinalized)
            {
                throw new InvalidOperationException("Cannot update a finalized bag");
            }

            existingBag.IsFinalized = updatedBag.IsFinalized;*/

            return await _bagRepo.UpdateAsync(existingBag.Id, existingBag);
        }
        public async Task<bool> DeleteBagAsync(string id)
        {
            var bag = await _bagRepo.GetBagByIdAsync(id);
            if (bag == null)
            {
                throw new ArgumentException("Bag not found");
            }

            /* if (bag.IsFinalized)
             {
                 throw new InvalidOperationException("Cannot delete a finalized bag");
             }*/

            return await _bagRepo.DeleteAsync(id);
        }
    }
}
