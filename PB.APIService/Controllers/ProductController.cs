using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;
using PB.APIService.RequestModel;


namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        Swp391Context dbc;
        public ProductController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _unitOfWork.ProductsRepository.GetAllAsync();
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductRequest productrequest)
        {
            var product = new Product
            {
                Id = productrequest.Id,
                Name = productrequest.Name,
                Price = productrequest.Price,
                CategoryId = productrequest.CategoryId,
                Rating = productrequest.Rating,
                StoreId = productrequest.StoreId,
                Stock = productrequest.Stock,
                
                Description = productrequest.Description
                
            };
            try
            {
                await _unitOfWork.ProductsRepository.CreateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductRequest productrequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            product.Description = productrequest.Description;
            product.Name = productrequest.Name;
            product.Price = productrequest.Price;
            product.CategoryId = productrequest.CategoryId;
            product.Rating = productrequest.Rating;
            product.StoreId = productrequest.StoreId;
            product.Stock = productrequest.Stock;
            try
            {
                await _unitOfWork.ProductsRepository.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _unitOfWork.ProductsRepository.RemoveAsync(product);

            return NoContent();
        }       
        private bool ProductExists(int id)
        {
            return _unitOfWork.ProductsRepository.GetByIdAsync(id) != null;
        }
        
    }
}