using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.StoreController;
using Type = LibraryInANutShell.Models.Type;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class TypeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public TypeController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryInANutShell.Models.Type>>> GetType()
        {
            return await _unitOfWork.TypeRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryInANutShell.Models.Type>> GetTypeById(int id)
        {
            var type = await _unitOfWork.TypeRepository.GetByIdAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot(TypeDTO slot)
        {
            var newStore = new Type
            {
                Id = slot.Id,
                Name = slot.Name,
                Capacity = slot.Capacity,
            };

            await _unitOfWork.TypeRepository.CreateAsync(newStore);
            await _unitOfWork.TypeRepository.SaveAsync();

            return CreatedAtAction(nameof(GetTypeById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeById(int id)
        {
            var type = await _unitOfWork.TypeRepository.GetByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.TypeRepository.RemoveAsync(type);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Type.");
        }
        public class TypeDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int? Capacity { get; set; }
        }

    }
}
