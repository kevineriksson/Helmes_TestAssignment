using PostOfficeAPI.Models;
using PostOfficeAPI.Models.Dto;

namespace PostOfficeAPI.Contracts.Services
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> GetShipmentByIdAsync(string id);
        Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto);
        Task<bool> UpdateShipmentAsync(Shipment shipment);
        Task<bool> DeleteShipmetAsync(string id);
        Task FinalizeShipmentAsync(string id);
    }
}
