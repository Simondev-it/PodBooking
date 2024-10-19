using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public BookingController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            return await _unitOfWork.BookingRepository.GetAllAsync();
        }
        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        // POST: api/bookings
        [HttpPost]
        public async Task<ActionResult<BookingDTO>> PostBooking(BookingRequest bookingRequest)
        {
            // Kiểm tra danh sách SlotIds
            if (bookingRequest.SlotIds == null || !bookingRequest.SlotIds.Any())
            {
                return BadRequest("Danh sách SlotId không thể trống.");
            }

            // Danh sách để lưu các Slot đã được tìm thấy
            var slots = new List<Slot>();

            // Lặp qua danh sách SlotIds để tìm các Slot tương ứng
            foreach (var slotId in bookingRequest.SlotIds)
            {
                var existingSlot = await _unitOfWork.SlotRepository.GetByIdAsync(slotId);
                if (existingSlot == null)
                {
                    return NotFound($"Slot với ID {slotId} không tồn tại.");
                }
                slots.Add(existingSlot);
            }

            // Tạo đối tượng Booking
            var booking = new Booking
            {
                Id = bookingRequest.Id,
                Date = bookingRequest.Date,
                Status = bookingRequest.Status,
                Feedback = bookingRequest.Feedback,
                PodId = bookingRequest.PodId,
                UserId = bookingRequest.UserId,
                Slots = slots // Gán danh sách slots đã tìm thấy vào booking
            };

            // Lưu booking vào cơ sở dữ liệu
            try
            {
                await _unitOfWork.BookingRepository.CreateAsync(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi tạo booking.");
            }

            // Tạo BookingDTO để trả về
            var bookingDTO = new BookingDTO
            {
                Id = booking.Id,
                Date = (DateTime)booking.Date,
                Status = booking.Status,
                Feedback = booking.Feedback,
                PodId = booking.PodId,
                UserId = booking.UserId,
                SlotIds = booking.Slots.Select(s => s.Id).ToList() // Chỉ lấy ID của slot
            };

            // Trả về 201 Created với thông tin booking
            return CreatedAtAction(nameof(GetBooking), new { id = bookingDTO.Id }, bookingDTO);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, BookingRequest bookingRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            booking.Date = bookingRequest.Date;
            booking.Status = bookingRequest.Status;
            booking.PodId = bookingRequest.PodId;
            booking.Feedback = bookingRequest.Feedback;
            booking.UserId  = bookingRequest.UserId;
            try
            {
                await _unitOfWork.BookingRepository.UpdateAsync(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                // Tìm Booking theo ID
                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
                if (booking == null)
                {
                    return NotFound(new { message = "Booking không tồn tại." });
                }

                // Xóa Booking
                await _unitOfWork.BookingRepository.RemoveAsync(booking);

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Kiểm tra xem lỗi có liên quan đến khóa ngoại không
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    // Trả về lỗi rõ ràng cho người dùng
                    return BadRequest(new
                    {
                        message = "Không thể xóa Booking do còn SlotBookings liên quan. Hãy xóa các SlotBookings trước."
                    });
                }

                // Xử lý các lỗi khác
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xóa Booking.", details = ex.Message });
            }
        }








        private bool BookingExists(int id)
        {
            return _unitOfWork.BookingRepository.GetByIdAsync(id) != null;
        }
    }
}
