using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using PostOfficeAPI.Contracts.Services;
using PostOfficeAPI.Models;
using PostOfficeAPI.Services;

namespace PostOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;

        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Bag>>> GetAllBags()
        {
            var bags = await _bagService.GetAllBagsAsync();
            if (bags == null || !bags.Any())
                return NotFound("No bags found.");
            return Ok(bags);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bag>> GetBagById(string id)
        {
            var bag = await _bagService.GetBagByIdAsync(id);
            if (bag == null) return NotFound();
            return Ok(bag);
        }
        [HttpGet("getBagsByShipmentId/{id}")]
        public async Task<ActionResult<Bag>> GetBagsByShipmentId(string id)
        {
            var bags = await _bagService.GetBagsByShimpentId(id);
            if (bags == null || !bags.Any())
                return NotFound("No bags found.");
            return Ok(bags);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBag(string id, [FromBody] Bag bag)
        {
            if (id != bag.Id)
                return BadRequest("ID mismatch");

            var success = await _bagService.UpdateBagAsync(bag);
            if (!success)
                return NotFound($"Bag with Id = {id} not found.");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBag(string id)
        {
            var success = await _bagService.DeleteBagAsync(id);
            if (!success)
                return NotFound($"Bag with Id = {id} not found.");

            return NoContent();
        }
        [HttpPost("createParcelBag")]
        public async Task<ActionResult<BagWithParcels>> CreateParcelBag([FromBody] BagWithParcels bag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBag = await _bagService.CreateBagAsync(bag);
            return CreatedAtAction(nameof(GetBagById), new { id = createdBag.Id }, createdBag);
        }

        [HttpPost("createLetterBag")]
        public async Task<ActionResult<BagWithLetters>> CreateLetterBag([FromBody] BagWithLetters bag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBag = await _bagService.CreateBagAsync(bag);
            return CreatedAtAction(nameof(GetBagById), new { id = createdBag.Id }, createdBag);
        }

    }
}
