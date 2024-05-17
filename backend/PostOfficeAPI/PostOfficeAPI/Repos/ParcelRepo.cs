using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.Contracts.Repos;
using PostOfficeAPI.Data;

namespace PostOfficeAPI.Repos
{
    public class ParcelRepo : BaseRepo<Parcel>, IParcelRepo
    {
        public ParcelRepo(AppDbContext dbContext) : base(dbContext) { }
        public async Task<Parcel> GetParcelByIdAsync(string Id) 
            => await _dbSet.FirstOrDefaultAsync(p => p.Id == Id);
        public async Task<List<Parcel>> GetParcelsByDestinationAsync(string destination) 
            => await _dbSet.Where(s => s.DestinationCountry == destination).ToListAsync();
        public async Task<List<Parcel>> GetParcelsByBagIdAsync(string bagId) 
            => await _dbSet.Where(a => a.BagId == bagId).ToListAsync();
        public async Task<IEnumerable<Parcel>> GetAllParcelsAsync()
            => await _dbSet.ToListAsync();

        public async Task<Parcel> CreateParcelAsync(ParcelDto parcelDto)
        {
            var parcel = new Parcel
            {
                Id = parcelDto.Id,
                RecipientName = parcelDto.RecipientName,
                DestinationCountry = parcelDto.DestinationCountry,
                Weight = parcelDto.Weight,
                Price = parcelDto.Price,
                BagId = parcelDto.BagId
            };

            _dbSet.Add(parcel);
            await _dbContext.SaveChangesAsync();
            return parcel;
        }
    }
}
