

using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.BookingOrderController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(CategoryDTO slot)
        {
            var newCategory = new Category
            {
                Id = slot.Id,
                Name = slot.Name,
                Status = slot.Status,

            };

            await _unitOfWork.CategoryRepository.CreateAsync(newCategory);
            await _unitOfWork.CategoryRepository.SaveAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.CategoryRepository.RemoveAsync(category);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Category.");
        }
        public class CategoryDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public string? Status { get; set; }

        }
    }
}
