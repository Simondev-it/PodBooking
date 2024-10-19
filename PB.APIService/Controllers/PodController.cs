using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391.Models;
using PodBooking.SWP391;
using PodBooking.SWP391.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<PodDTO>> PostBooking(PodRequest podRequest)
        {
            // Kiểm tra danh sách SlotIds
            if (podRequest.UtilityId == null || !podRequest.UtilityId.Any())

            {
                return BadRequest("Danh sách Utility không thể trống.");
            }

            // Danh sách để lưu các Slot đã được tìm thấy
            var utilitys = new List<PodBooking.SWP391.Models.Utility>();

            // Lặp qua danh sách SlotIds để tìm các Slot tương ứng
            foreach (var utilityid in podRequest.UtilityId)
            {
                var existingSlot = await _unitOfWork.UtilityRepository.GetByIdAsync(utilityid);
                if (existingSlot == null)
                {
                    return NotFound($"Slot với ID {utilityid} không tồn tại.");
                }
                utilitys.Add(existingSlot);
            }

            // Tạo đối tượng Booking
            var pod  = new Pod
            {
                Id = podRequest.Id,
                Name = podRequest.Name,
                Image = podRequest.Image,
                Description = podRequest.Description,
                Rating = podRequest.Rating,
                Status = podRequest.Status,
                TypeId = podRequest.TypeId,
                StoreId = podRequest.StoreId,
                Utilities = utilitys

                // Gán danh sách slots đã tìm thấy vào booking
            };

            // Lưu booking vào cơ sở dữ liệu
            try
            {
                await _unitOfWork.PodRepository.CreateAsync(pod);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi tạo booking.");
            }

            // Tạo BookingDTO để trả về
            var poddto = new PodDTO
            {
                Id = pod.Id,
                Name = pod.Name,
                Image= pod.Image,
                Description = pod.Description,
                Rating = pod.Rating,
                Status = pod.Status,
                TypeId = pod.TypeId,
                StoreId = pod.StoreId,

                UtilityId = pod.Utilities.Select(s => s.Id).ToList() // Chỉ lấy ID của slot
            };

            // Trả về 201 Created với thông tin booking
            return CreatedAtAction(nameof(GetPod), new { id = poddto.Id }, poddto);
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
