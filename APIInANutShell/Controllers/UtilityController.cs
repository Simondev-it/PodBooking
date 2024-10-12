using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.UserController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class UtilityController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UtilityController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utility>>> GetUtility()
        {
            return await _unitOfWork.UtilityRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utility>> GetUtilityById(int id)
        {
            var utility = await _unitOfWork.UtilityRepository.GetByIdAsync(id);

            if (utility == null)
            {
                return NotFound();
            }

            return utility;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSlot(UtilityDTO slot)
        {
            var newUtility = new Utility
            {
               Id = slot.Id,
               Name = slot.Name,
               Image = slot.Image,
               Description = slot.Description,
            };

            await _unitOfWork.UtilityRepository.CreateAsync(newUtility);
            await _unitOfWork.UtilityRepository.SaveAsync();

            return CreatedAtAction(nameof(GetUtilityById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilityById(int id)
        {
            var utility = await _unitOfWork.UtilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.UtilityRepository.RemoveAsync(utility);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Utility.");
        }
        public class UtilityDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public string? Image { get; set; }

            public string? Description { get; set; }
        }
    }
}
