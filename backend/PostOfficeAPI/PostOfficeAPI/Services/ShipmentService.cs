using PostOfficeAPI.Contracts.Repos;
using PostOfficeAPI.Contracts.Services;
using PostOfficeAPI.Models;
using PostOfficeAPI.Models.Dto;
using PostOfficeAPI.Repos;

namespace PostOfficeAPI.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepo _shipmentRepo;
        public ShipmentService(IShipmentRepo shipmentRepo)
        {
            _shipmentRepo = shipmentRepo;
        }

        public async Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto)
        {
            return await _shipmentRepo.CreateShipmentAsync(shipmentDto);
        }

        public async Task<bool> DeleteShipmetAsync(string id)
        {
            var shipment = await _shipmentRepo.GetShipmentByIdAsync(id);
            if (shipment != null)
            {
                var deletedShipment = await _shipmentRepo.DeleteAsync(shipment.Id);
                return deletedShipment;
            }
            return false;
        }

        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync()
        {
            return await _shipmentRepo.GetAllShipmentsAsync();
        }

        public async Task<Shipment> GetShipmentByIdAsync(string id)
        {
            return await _shipmentRepo.GetShipmentByIdAsync(id);
        }

        public async Task<bool> UpdateShipmentAsync(Shipment shipment)
        {
            shipment = await _shipmentRepo.GetShipmentByIdAsync(shipment.Id);

            if (shipment == null)
            {
                throw new ArgumentException("Shipment not found");
            }

            var success = await _shipmentRepo.UpdateAsync(shipment.Id, shipment);
            if (!success)
            {
                throw new InvalidOperationException("Failed to update the shipment");
            }

            return success;
        }
        public async Task FinalizeShipmentAsync(string id)
        {
            var shipment = await _shipmentRepo.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                throw new KeyNotFoundException("Shipment not found");
            }
            await _shipmentRepo.FinalizeShipmentAsync(id);
        }
    }
}
