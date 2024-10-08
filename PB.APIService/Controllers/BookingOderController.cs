using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingOderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        public BookingOderController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/BookingOrder
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingOrder>>> GetBookingOrder()
        {
            return await _unitOfWork.BookingOrderRepository.GetAllAsync();
        }
        // GET: api/BookingOrder/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingOrder>> GetBookingOder(int id)
        {
            var bookingOrder = await _unitOfWork.BookingOrderRepository.GetByIdAsync(id);

            if (bookingOrder == null)
            {
                return NotFound();
            }

            return bookingOrder;
        }
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<BookingOrder>> PostBookingOrder(BookingOderRequest bookingOrderRequest)
        {
            var bookingOder = new BookingOrder
            {
                Id = bookingOrderRequest.Id,
                Amount = bookingOrderRequest.Amount,
                Quantity = bookingOrderRequest.Quantity,
                Status  = bookingOrderRequest.Status,   
                Date = bookingOrderRequest.Date,    
                BookingId = bookingOrderRequest.BookingId,
                ProductId = bookingOrderRequest.ProductId,  



            };
            try
            {
                await _unitOfWork.BookingOrderRepository.CreateAsync(bookingOder);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetBookingOrder", new { id = bookingOder.Id }, bookingOder);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingOder(int id, BookingOderRequest bookingOderRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var bookingOrder = await _unitOfWork.BookingOrderRepository.GetByIdAsync(id);
            if (bookingOrder == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            bookingOrder.Amount = bookingOderRequest.Amount;
            bookingOrder.Quantity = bookingOderRequest.Quantity;
            bookingOrder.Status = bookingOderRequest.Status;
            bookingOrder.Date = bookingOderRequest.Date;
            bookingOrder.BookingId = bookingOderRequest.BookingId;
            bookingOrder.ProductId = bookingOderRequest.ProductId;
            
            try
            {
                await _unitOfWork.BookingOrderRepository.UpdateAsync(bookingOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingOrderExists(id))
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
        public async Task<IActionResult> DeleteBookingOrder(int id)
        {
            var bookingOrder = await _unitOfWork.BookingOrderRepository.GetByIdAsync(id);
            if (bookingOrder == null)
            {
                return NotFound();
            }

            await _unitOfWork.BookingOrderRepository.RemoveAsync(bookingOrder);

            return NoContent();
        }
        private bool BookingOrderExists(int id)
        {
            return _unitOfWork.BookingOrderRepository.GetByIdAsync(id) != null;
        }
    }
}

   
