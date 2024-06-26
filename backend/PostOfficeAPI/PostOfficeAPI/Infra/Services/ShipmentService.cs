﻿using PostOfficeAPI.ApplicationCore.Contracts.Repos;
using PostOfficeAPI.ApplicationCore.Contracts.Services;
using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.ApplicationCore.Models.Dto;

namespace PostOfficeAPI.Infra.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepo _shipmentRepo;
        public ShipmentService(IShipmentRepo shipmentRepo)
        {
            _shipmentRepo = shipmentRepo;
        }
        public async Task<IEnumerable<Shipment>> GetAllShipmentsAsync() => await _shipmentRepo.GetAllShipmentsAsync();
        public async Task<Shipment> GetShipmentByIdAsync(string id) => await _shipmentRepo.GetShipmentByIdAsync(id);
        public async Task<Shipment> CreateShipmentAsync(ShipmentDto shipmentDto) => await _shipmentRepo.CreateShipmentAsync(shipmentDto);
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
