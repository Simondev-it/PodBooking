using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _unitOfWork.ProductsRepository.GetAllAsync();
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                await _unitOfWork.ProductsRepository.CreateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
    }
}
