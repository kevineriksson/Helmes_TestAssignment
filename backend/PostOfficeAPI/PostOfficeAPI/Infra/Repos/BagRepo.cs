using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.Infra.Data;

namespace PostOfficeAPI.Infra.Repos
{
    public class BagRepo : BaseRepo<Bag>, IBagRepo
    {
        public BagRepo(AppDbContext dbContext) : base(dbContext) { }
        public async Task<List<Bag>> GetBagsByShipmentIdAsync(string shipmentId)
            => await _dbSet
                .Where(a => a.ShipmentId == shipmentId)
                .ToListAsync();
        public async Task<List<BagWithParcels>> GetBagsWithParcelsAsync(Parcel parcel)
            => await _dbSet.OfType<BagWithParcels>()
                .Where(a => a.Id == parcel.Id)
                .Include(b => b.Parcels)
                .ToListAsync();
        public async Task<List<BagWithLetters>> GetBagsWithLettersAsync()
            => await _dbSet.OfType<BagWithLetters>().ToListAsync();

        public async Task<Bag> GetBagByIdAsync(string id)
            => await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<IEnumerable<Bag>> GetAllBagsAsync()
            => await _dbSet.ToListAsync();
    }
}
