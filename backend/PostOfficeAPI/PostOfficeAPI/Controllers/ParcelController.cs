using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostOfficeAPI.Contracts.Services;

namespace PostOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IParcelService _parcelService;
        public ParcelController(IParcelService parcelService)
        {
            _parcelService = parcelService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Parcel>>> GetAllParcels()
        {
            var parcels = await _parcelService.GetAllParcelsAsync();
            if (parcels == null) return NotFound();
            return Ok(parcels);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Parcel>>> GetParcelsById(string id)
        {
            var parcel = await _parcelService.GetParcelsByBagIdAsync(id);
            if (parcel == null) return NotFound();
            return Ok(parcel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParcel(string id, [FromBody] Parcel parcel)
        {
            if (id != parcel.Id)
                return BadRequest("ID mismatch");

            var success = await _parcelService.UpdateParcelAsync(parcel);
            if (!success)
                return NotFound($"Parcel with Id = {id} not found.");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcel(string id)
        {
            var success = await _parcelService.DeleteParcelAsync(id);
            if (!success)
                return NotFound($"Parcel with Id = {id} not found.");

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult> CreateParcel([FromBody] ParcelDto parcelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdParcel = await _parcelService.CreateParcelAsync(parcelDto);
            return CreatedAtAction(nameof(GetParcelsById), new { id = createdParcel.Id }, createdParcel);
        }
    }
}
