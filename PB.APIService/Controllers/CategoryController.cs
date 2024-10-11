using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public CategoryController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingOrder>>> GetBookingOrder()
        {
            return await _unitOfWork.BookingOrderRepository.GetAllAsync();
        }
        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }
        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryRequest categoryRequest)
        {
            var category = new Category
            {
                Id = categoryRequest.Id,
                Name = categoryRequest.Name,
                Status = categoryRequest.Status,
                
            };
            try
            {
                await _unitOfWork.CategoryRepository.CreateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryRequest categoryRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            category.Name = categoryRequest.Name;
            category.Status = categoryRequest.Status;
            

            try
            {
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var catecory = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (catecory == null)
            {
                return NotFound();
            }

            await _unitOfWork.CategoryRepository.RemoveAsync(catecory);

            return NoContent();
        }
        private bool CategoryExists(int id)
        {
            return _unitOfWork.CategoryRepository.GetByIdAsync(id) != null;
        }
    }
}
