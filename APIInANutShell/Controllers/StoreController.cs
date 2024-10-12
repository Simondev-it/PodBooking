using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.SlotController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class StoreController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public StoreController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore()
        {
            return await _unitOfWork.StoreRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStoreById(int id)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot(StoreDTO slot)
        {
            var newStore = new Store
            {
                Id = slot.Id,
                Name = slot.Name,
                Address = slot.Address,
                Contact = slot.Contact,
            };

            await _unitOfWork.StoreRepository.CreateAsync(newStore);
            await _unitOfWork.StoreRepository.SaveAsync();

            return CreatedAtAction(nameof(GetStoreById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreById(int id)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.StoreRepository.RemoveAsync(store);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Store.");
        }

        public class StoreDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public string? Address { get; set; }

            public string? Contact { get; set; }
        }
    }
}