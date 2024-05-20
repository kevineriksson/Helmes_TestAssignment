using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.ApplicationCore.Models.Dto;

namespace PostOfficeAPI.ApplicationCore.Contracts.Services
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
