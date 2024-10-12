using LibraryInANutShell;
using LibraryInANutShell.Models;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.PaymentController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class PodController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public PodController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pod>>> GetPod()
        {
            return await _unitOfWork.PodRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pod>> GetPodById(int id)
        {
            var pod = await _unitOfWork.PodRepository.GetByIdAsync(id);

            if (pod == null)
            {
                return NotFound();
            }

            return pod;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePod(PodDTO slot)
        {
            var newPod = new Pod
            {
                Id = slot.Id,
                Name = slot.Name,
                Image = slot.Image,
                Description = slot.Description,
                Rating = slot.Rating,
                Status = slot.Status,
                TypeId = slot.TypeId,
                StoreId = slot.StoreId,
            };

            await _unitOfWork.PodRepository.CreateAsync(newPod);
            await _unitOfWork.PodRepository.SaveAsync();

            return CreatedAtAction(nameof(GetPodById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePodById(int id)
        {
            var pod = await _unitOfWork.PodRepository.GetByIdAsync(id);
            if (pod == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.PodRepository.RemoveAsync(pod);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Category.");
        }

        public class PodDTO
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public string? Image { get; set; }

            public string? Description { get; set; }

            public int? Rating { get; set; }

            public string? Status { get; set; }

            public int TypeId { get; set; }

            public int StoreId { get; set; }
        }

    }
}
