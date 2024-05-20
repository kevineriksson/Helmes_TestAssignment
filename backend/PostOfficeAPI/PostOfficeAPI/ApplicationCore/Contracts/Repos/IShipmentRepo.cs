using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.ApplicationCore.Models.Dto;

namespace PostOfficeAPI.ApplicationCore.Contracts.Repos
{
    public interface IShipmentRepo : IRepo<Shipment>
    {
        Task<Shipment> GetShipmentByIdAsync(string id);
        Task<IEnumerable<Shipment>> GetAllShipmentsAsync();
        Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto);
        Task FinalizeShipmentAsync(string id);
    }
}
