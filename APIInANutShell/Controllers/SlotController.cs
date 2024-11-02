using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.ModelRequest;
using LibraryInANutShell.Models;
using LibraryInANutShell;
using LibraryInANutShell.Models;
using LibraryInANutShell;
using PB.APIService.ModelRequest;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public SlotController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        // GET: api/Slot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Slot>>> GetSlot()
        {
            return await _unitOfWork.SlotRepository.GetAllAsync();
        }
        // GET: api/Pod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Slot>> GetSlot(int id)
        {
            var slot = await _unitOfWork.SlotRepository.GetByIdAsync(id);

            if (slot == null)
            {
                return NotFound();
            }

            return slot;
        }
        // POST: api/Slot
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Slot>> PostSlot(SlotRequest slotRequest)
        {
            var slot = new Slot
            {
                Id = slotRequest.Id,
                Name = slotRequest.Name,
                StartTime = slotRequest.StartTime,
                EndTime = slotRequest.EndTime,
                Price = slotRequest.Price,
                Status = slotRequest.Status,
                PodId = slotRequest.PodId,




            };
            try
            {
                await _unitOfWork.SlotRepository.CreateAsync(slot);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetSlot", new { id = slot.Id }, slot);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSlot(int id, SlotRequest slotRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var slot = await _unitOfWork.SlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            slot.Name = slotRequest.Name;
            slot.StartTime = slotRequest.StartTime;
            slot.EndTime = slotRequest.EndTime;
            slot.Price = slotRequest.Price;
            slot.Status = slotRequest.Status;
            slot.PodId = slotRequest.PodId;



            try
            {
                await _unitOfWork.SlotRepository.UpdateAsync(slot);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // DELETE: api/Slot/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlot(int id)
        {
            var slot = await _unitOfWork.SlotRepository.GetByIdAsync(id);
            if (slot == null)
            {
                return NotFound();
            }

            await _unitOfWork.SlotRepository.RemoveAsync(slot);

            return NoContent();
        }
        private bool SlotExists(int id)
        {
            return _unitOfWork.SlotRepository.GetByIdAsync(id) != null;
        }
    }
}
