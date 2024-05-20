using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostOfficeAPI.ApplicationCore.Contracts.Services;
using PostOfficeAPI.ApplicationCore.Models;
using PostOfficeAPI.ApplicationCore.Models.Dto;

namespace PostOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Shipment>>> GetAllShipments()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            if (shipments == null) return NotFound();
            return Ok(shipments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Shipment>> GetShipmentByIdAsync(string id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null) return NotFound();
            return Ok(shipment);
        }
        [HttpPost]
        public async Task<ActionResult> CreateShipment([FromBody] ShipmentDto shipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdShipment = await _shipmentService.CreateShipmentAsync(shipmentDto);
            return CreatedAtAction(nameof(GetAllShipments), new { id = createdShipment.Id }, createdShipment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShipment(string id, [FromBody] Shipment shipment)
        {
            if (id != shipment.Id)
                return BadRequest("ID mismatch");

            var success = await _shipmentService.UpdateShipmentAsync(shipment);
            if (!success)
                return NotFound($"Shipment with Id = {id} not found.");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipment(string id)
        {
            var success = await _shipmentService.DeleteShipmetAsync(id);
            if (!success)
                return NotFound($"Shipment with Id = {id} not found.");

            return NoContent();
        }
        [HttpPatch("{id}/finalize")]
        public async Task<IActionResult> FinalizeShipment(string id)
        {
            try
            {
                await _shipmentService.FinalizeShipmentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
