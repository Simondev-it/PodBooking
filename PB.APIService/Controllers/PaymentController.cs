using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PB.APIService.Services;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;

namespace PB.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IVnpayService _vpnpayService;
        public PaymentController(UnitOfWork unitOfWork, IVnpayService vnpayService)
        { 
            _unitOfWork = unitOfWork;
            _vpnpayService = vnpayService;

        }
        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment()
        {
            return await _unitOfWork.PaymentRepository.GetAllAsync();
        }
        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }
        // POST: api/Payment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754    
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentRequest paymentRequest)
        {
            var payment = new Payment
            {
                Id = paymentRequest.Id,
                Method = paymentRequest.Method,
                Amount = paymentRequest.Amount,
                Date= paymentRequest.Date,
                Status = paymentRequest.Status,
                BookingId = paymentRequest.BookingId,
                
            };
            try
            {
                await _unitOfWork.PaymentRepository.CreateAsync(payment);
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentRequest paymentRequest)
        {
            // Kiểm tra sản phẩm có tồn tại hay không
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound("Sản phẩm không tồn tại.");
            }

            payment.Method = paymentRequest.Method;
            payment.Amount = paymentRequest.Amount;
            payment.Date = paymentRequest.Date;
            payment.Status = paymentRequest.Status;
            payment.BookingId = paymentRequest.BookingId;
            


            try
            {
                await _unitOfWork.PaymentRepository.UpdateAsync(payment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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
        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _unitOfWork.PaymentRepository.RemoveAsync(payment);

            return NoContent();
        }
        private bool PaymentExists(int id)
        {
            return _unitOfWork.PaymentRepository.GetByIdAsync(id) != null;
        }
        //public IActionResult PaymentCallBack()
        //{
        //    return View();
        //}

        [HttpPost("create")]
        public IActionResult CreatePayment([FromBody] VnPaymentRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Request cannot be null.");
            }

            // Tạo URL thanh toán
            var paymentUrl = _vpnpayService.CreatePaymentUrl(HttpContext, model);

            return Ok(new { PaymentUrl = paymentUrl });
        }
    }
}
