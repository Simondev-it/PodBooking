using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391.Models;
using PodBooking.SWP391;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public UtilityController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/utility
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utility>>> GetUtility()
        {
            return await _unitOfWork.UtilityRepository.GetAllAsync();
        }
        // GET: api/utility/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utility>> GetUtility(int id)
        {
            var utility = await _unitOfWork.UtilityRepository.GetByIdAsync(id);

            if (utility == null)
            {
                return NotFound();
            }

            return utility;
        }
        // POST: api/utility
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Utility>> PostUtility(UtilityRequest utilityRequest)
        {
            var utility = new Utility
            {
                Id = utilityRequest.Id,
                Name = utilityRequest.Name,
                Description = utilityRequest.Description,
                Image = utilityRequest.Image,
                
            };
            try
            {
                await _unitOfWork.UtilityRepository.CreateAsync(utility);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetUtility", new { id = utility.Id }, utility);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtility(int id, UtilityRequest utilityRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var utility = await _unitOfWork.UtilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            utility.Name = utilityRequest.Name;
            utility.Description = utilityRequest.Description;
            utility.Image = utilityRequest.Image;


            
            try
            {
                await _unitOfWork.UtilityRepository.UpdateAsync(utility);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilityExists(id))
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
        // DELETE: api/utility/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtility(int id)
        {
            var utility = await _unitOfWork.UtilityRepository.GetByIdAsync(id);
            if (utility == null)
            {
                return NotFound();
            }

            await _unitOfWork.UtilityRepository.RemoveAsync(utility);

            return NoContent();
        }
        private bool UtilityExists(int id)
        {
            return _unitOfWork.UtilityRepository.GetByIdAsync(id) != null;
        }
    }
}
