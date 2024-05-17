using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.Contracts.Repos;
using PostOfficeAPI.Data;
using PostOfficeAPI.Models;

namespace PostOfficeAPI.Repos
{
    public class BagRepo : BaseRepo<Bag>, IBagRepo
    {
        public BagRepo(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Bag>> GetBagsByShipmentId (string shipmentId)
            => await _dbSet
                .Where(a => a.ShipmentId == shipmentId)
                .ToListAsync();
        public async Task<List<BagWithParcels>> GetBagsWithParcels(Parcel parcel)
            => await _dbSet.OfType<BagWithParcels>()
                .Where(a => a.Id == parcel.Id)
                .Include(b => b.Parcels)
                .ToListAsync();
        public async Task<List<BagWithLetters>> GetBagsWithLetters()
            => await _dbSet.OfType<BagWithLetters>().ToListAsync();

        public async Task<Bag> GetBagByIdAsync(string id)
            => await _dbSet.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Bag>> GetAllBagsAsync()
            => await _dbSet.ToListAsync();
    }
}
