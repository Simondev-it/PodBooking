using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.PodController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPost]
        public async Task<ActionResult> CreatePod(ProductDTO slot)
        {
            var newProduct = new Product
            {
                Id = slot.Id,
                Name = slot.Name,
                Price = slot.Price,
                Description = slot.Description,
                Rating = slot.Rating,
                Stock = slot.Stock,
                StoreId = slot.StoreId,
                CategoryId = slot.CategoryId,
            };

            await _unitOfWork.ProductRepository.CreateAsync(newProduct);
            await _unitOfWork.ProductRepository.SaveAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.ProductRepository.RemoveAsync(product);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Product.");
        }
        public class ProductDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public int? Price { get; set; }

            public string? Description { get; set; }

            public int? Rating { get; set; }

            public int? Stock { get; set; }

            public int StoreId { get; set; }

            public int CategoryId { get; set; }
        }
    }
}
