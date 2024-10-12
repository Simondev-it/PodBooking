using LibraryInANutShell;
using LibraryInANutShell.Models;
using LibraryInANutShell.Repositories;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.SlotController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class BookingController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public BookingController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
            return await _unitOfWork.BookingRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBooking(BookingDTO slot)
        {
            var newBooking = new Booking
            {
                Id = slot.Id,
                Feedback = slot.Feedback,
                Date = slot.Date,
                Status = slot.Status,
                PodId = slot.PodId,
                UserId = slot.UserId,

            };

            await _unitOfWork.BookingRepository.CreateAsync(newBooking);
            await _unitOfWork.BookingRepository.SaveAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingById(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.BookingRepository.RemoveAsync(booking);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa booking.");
        }
        public class BookingDTO
        {
            public int Id { get; set; }

            public DateOnly? Date { get; set; }

            public string? Status { get; set; }

            public string? Feedback { get; set; }

            public int PodId { get; set; }

            public int UserId { get; set; }

        }
    }
}
