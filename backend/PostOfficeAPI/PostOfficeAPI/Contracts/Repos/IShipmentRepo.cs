using PostOfficeAPI.Models;
using PostOfficeAPI.Models.Dto;

namespace PostOfficeAPI.Contracts.Repos
{
    public interface IShipmentRepo : IRepo<Shipment>
    {
        Task<Shipment> GetShipmentByIdAsync(string id);
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto);
        Task FinalizeShipmentAsync(string id);
    }
}
