using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391.Models;
using PodBooking.SWP391;
using Type = PodBooking.SWP391.Models.Type;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public TypeController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/Type
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Type>>> GetType()
        {
            return await _unitOfWork.TypeRepository.GetAllAsync();
        }
        // GET: api/Type/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Type>> GetType(int id)
        {
            var type = await _unitOfWork.TypeRepository.GetByIdAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }
        // POST: api/Type
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Type>> PostType(TypeRequest typeRequest)
        {
            var type = new Type
            {
                Id = typeRequest.Id,
                Name = typeRequest.Name,
                Capacity = typeRequest.Capacity,
                
            };
            try
            {
                await _unitOfWork.TypeRepository.CreateAsync(type);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GeType", new { id = type.Id }, type);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutType(int id,TypeRequest typeRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var type = await _unitOfWork.TypeRepository.GetByIdAsync(id);
            if (type == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            type.Name = typeRequest.Name;
            type.Capacity = typeRequest.Capacity;
            
            try
            {
                await _unitOfWork.TypeRepository.UpdateAsync(type);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeExists(id))
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
        // DELETE: api/Type/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await _unitOfWork.TypeRepository.GetByIdAsync(id);
            if (type == null)
            {
                return NotFound();
            }

            await _unitOfWork.TypeRepository.RemoveAsync(type);

            return NoContent();
        }
        private bool TypeExists(int id)
        {
            return _unitOfWork.TypeRepository.GetByIdAsync(id) != null;
        }
    }
}
