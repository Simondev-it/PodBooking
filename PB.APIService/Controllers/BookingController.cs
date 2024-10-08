using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Booking>> GetUser(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingRequest bookingRequest)
        {
            var booking = new Booking
            {
                Id = bookingRequest.Id,
                Date = bookingRequest.Date,
                Status = bookingRequest.Status,
                Feedback = bookingRequest.Feedback,
                PodId=bookingRequest.PodId,
                UserId=bookingRequest.UserId,



            };
            try
            {
                await _unitOfWork.BookingRepository.CreateAsync(booking);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
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
        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            await _unitOfWork.BookingRepository.RemoveAsync(booking);

            return NoContent();
        }
        private bool BookingExists(int id)
        {
            return _unitOfWork.BookingRepository.GetByIdAsync(id) != null;
        }
    }
}
