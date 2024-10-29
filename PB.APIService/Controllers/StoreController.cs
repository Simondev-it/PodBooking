using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391.Models;
using PodBooking.SWP391;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public StoreController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStore()
        {
            return await _unitOfWork.StoreRepository.GetAllAsync();
        }
        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }
        // POST: api/store
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(StoreRequest storeRequest)
        {
            var store = new Store
            {
                Id = storeRequest.Id,
                Name = storeRequest.Name,
                Status = storeRequest.Status,
                Address = storeRequest.Address,
                Contact = storeRequest.Contact,
                
            };
            try
            {
                await _unitOfWork.StoreRepository.CreateAsync(store);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, StoreRequest storeRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            store.Name = storeRequest.Name;
            store.Address = storeRequest.Address;
            store.Contact = storeRequest.Contact;
            store.Status = storeRequest.Status;
            
            try
            {
                await _unitOfWork.StoreRepository.UpdateAsync(store);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
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
        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            await _unitOfWork.StoreRepository.RemoveAsync(store);

            return NoContent();
        }
        private bool StoreExists(int id)
        {
            return _unitOfWork.StoreRepository.GetByIdAsync(id) != null;
        }
    }
}
