using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class SlotController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public SlotController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> GetSlot()
        {
            return await _unitOfWork.SlotRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlotById(int id)
        {
            var slot = await _unitOfWork.SlotRepository.GetByIdAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot(SlotDTO slot)
        {
            var newslot = new Slot
            {
                Id = slot.Id,
                Name = slot.Name,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                Price = slot.Price,
                Status = slot.Status,
                PodId = slot.PodId,
            };

            await _unitOfWork.SlotRepository.CreateAsync(newslot);
            await _unitOfWork.SlotRepository.SaveAsync();

            return CreatedAtAction(nameof(GetSlotById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlotById(int id)
        {
            var slot = await _unitOfWork.SlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.SlotRepository.RemoveAsync(slot);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Slot.");
        }
        public class SlotDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int? StartTime { get; set; }

            public int? EndTime { get; set; }

            public int? Price { get; set; }

            public string? Status { get; set; }

            public int PodId { get; set; }

        }

    }
}
