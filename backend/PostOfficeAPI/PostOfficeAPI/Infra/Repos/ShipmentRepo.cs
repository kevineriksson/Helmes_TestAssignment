using Microsoft.EntityFrameworkCore;
using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.ApplicationCore.Models.Dto;
using PostOfficeAPI.Infra.Data;

namespace PostOfficeAPI.Infra.Repos
{
    public class ShipmentRepo : BaseRepo<Shipment>, IShipmentRepo
    {
        public ShipmentRepo(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
            => await _dbSet.ToListAsync();
        public async Task<List<Shipment>> GetAllShipmentsWithBags()
            => await _dbSet.Include(dbSet => dbSet.Bags).ToListAsync();
        public async Task<Shipment> GetShipmentByIdAsync(string id)
            => await _dbSet.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto)
        {
            var shipment = new Shipment
            {
                Id = shipmentDto.Id,
                FlightNumber = shipmentDto.FlightNumber,
                AirportCode = shipmentDto.AirportCode,
                FlightDate = shipmentDto.FlightDate
            };

            _dbSet.Add(shipment);
            await _dbContext.SaveChangesAsync();
            return shipment;
        }
        public async Task FinalizeShipmentAsync(string id)
        {
            var shipment = await GetShipmentByIdAsync(id);
            if (shipment != null)
            {
                shipment.isFinalized = true;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
