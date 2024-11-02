using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.ModelRequest;
using LibraryInANutShell.Models;
using LibraryInANutShell;
using LibraryInANutShell.Repositories;
using LibraryInANutShell.Models;
using LibraryInANutShell;
using PB.APIService.ModelRequest;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public PodController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        // GET: api/Pod
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pod>>> GetPod()
        {
            return await _unitOfWork.PodRepository.GetAllAsync();
        }
        // GET: api/Pod/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pod>> GetPod(int id)
        {
            var pod = await _unitOfWork.PodRepository.GetByIdAsync(id);

            if (pod == null)
            {
                return NotFound();
            }

            return pod;
        }
        // POST: api/Pod
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Pod>> PostPod(PodRequest podRequest)
        {
            var pod = new Pod
            {
                Id = podRequest.Id,
                Name = podRequest.Name,
                Image = podRequest.Image,
                Description = podRequest.Description,
                Rating = podRequest.Rating,
                Status = podRequest.Status,
                TypeId = podRequest.TypeId,
                StoreId = podRequest.StoreId,


            };
            try
            {
                await _unitOfWork.PodRepository.CreateAsync(pod);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetPod", new { id = pod.Id }, pod);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPod(int id, PodRequest podRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var pod = await _unitOfWork.PodRepository.GetByIdAsync(id);
            if (pod == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            pod.Name = podRequest.Name;
            pod.Image = podRequest.Image;
            pod.Description = podRequest.Description;
            pod.Rating = podRequest.Rating;
            pod.Status = podRequest.Status;
            pod.TypeId = podRequest.TypeId;
            pod.StoreId = podRequest.StoreId;


            try
            {
                await _unitOfWork.PodRepository.UpdateAsync(pod);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodExists(id))
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
        // DELETE: api/Pod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePod(int id)
        {
            var pod = await _unitOfWork.PodRepository.GetByIdAsync(id);
            if (pod == null)
            {
                return NotFound();
            }

            await _unitOfWork.PodRepository.RemoveAsync(pod);

            return NoContent();
        }
        private bool PodExists(int id)
        {
            return _unitOfWork.PodRepository.GetByIdAsync(id) != null;
        }

    }
}
