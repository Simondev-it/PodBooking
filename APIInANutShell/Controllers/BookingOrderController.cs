using LibraryInANutShell;
using LibraryInANutShell.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class BookingOrderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public BookingOrderController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingOrder>>> GetBookingOrder()
        {
            return await _unitOfWork.BookingOrderRepository.GetAllAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BookingOrder>> GetBookingOrderById(int id)
        {
            var bookingOrder = await _unitOfWork.BookingOrderRepository.GetByIdAsync(id);

            if (bookingOrder == null)
            {
                return NotFound();
            }

            return bookingOrder;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBookingOrder(BookingOrderDTO slot)
        {
            var newBookingOrder = new BookingOrder
            {
                Id = slot.Id,
                Amount = slot.Amount,
                Quantity = slot.Quantity,
                Status = slot.Status,
                Date = slot.Date,
                BookingId = slot.BookingId,
                ProductId = slot.ProductId,

            };

            await _unitOfWork.BookingOrderRepository.CreateAsync(newBookingOrder);
            await _unitOfWork.BookingOrderRepository.SaveAsync();

            return CreatedAtAction(nameof(GetBookingOrderById), new { id = slot.Id }, slot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingOrderById(int id)
        {
            var bookingOrder = await _unitOfWork.BookingOrderRepository.GetByIdAsync(id);
            if (bookingOrder == null)
            {
                return NotFound(); 
            }

            var result = await _unitOfWork.BookingOrderRepository.RemoveAsync(bookingOrder);
            if (result)
            {
                return NoContent(); 
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa bookingOrder."); 
        }

        public class BookingOrderDTO
        {
            public int Id { get; set; }

            public int? Amount { get; set; }

            public int? Quantity { get; set; }

            public string? Status { get; set; }

            public DateOnly? Date { get; set; }

            public int BookingId { get; set; }

            public int ProductId { get; set; }
        }
    }
}
