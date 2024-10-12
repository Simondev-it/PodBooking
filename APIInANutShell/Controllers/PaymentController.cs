using LibraryInANutShell.Models;
using LibraryInANutShell;
using Microsoft.AspNetCore.Mvc;
using static APIInANutShell.Controllers.CategoryController;

namespace APIInANutShell.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PaymentController(UnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            return await _unitOfWork.PaymentRepository.GetAllAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreatePayment(PaymentDTO slot)
        {
            var newPayment = new Payment
            {
                Id = slot.Id,
                Method = slot.Method,
                Amount = slot.Amount,
                Status = slot.Status,
                Date = slot.Date,
                BookingId = slot.BookingId,
            };

            await _unitOfWork.PaymentRepository.CreateAsync(newPayment);
            await _unitOfWork.PaymentRepository.SaveAsync();

            return CreatedAtAction(nameof(GetPaymentById), new { id = slot.Id }, slot);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentById(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            var result = await _unitOfWork.PaymentRepository.RemoveAsync(payment);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(500, "Có lỗi xảy ra khi xóa Category.");
        }

        public class PaymentDTO
        {
            public int Id { get; set; }

            public string? Method { get; set; }

            public int? Amount { get; set; }

            public DateOnly? Date { get; set; }

            public string? Status { get; set; }

            public int BookingId { get; set; }
        }


    }
}
