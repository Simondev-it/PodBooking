using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB.APIService.RequestModel;
using PB.APIService.Services;
using PodBooking.SWP391;
using PodBooking.SWP391.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [HttpGet("booking/{bookingId}")]
        public async Task<ActionResult<Payment>> GetPaymentByBookingId(int bookingId)
        {
            var payment = await _unitOfWork.PaymentRepository.GetByBookingIdAsync(bookingId);

            if (payment == null)
            {
                return NotFound(new { Message = "Payment not found for the given BookingId." });
            }

            return Ok(payment);
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
        public async Task<IActionResult> CreatePayment([FromBody] VnPaymentRequestModel model)
        {
            if (model == null)
            {
                return BadRequest("Request cannot be null.");
            }

            // Tạo đối tượng Payment và lưu vào cơ sở dữ liệu
            var payment = new Payment
            {
                Id = model.Id,
                Method = "Thanh Toán Vnpay",
                Amount = model.Amount, // Kiểm tra kiểu dữ liệu
                Date = DateTime.Now,
                Status = "Chưa thanh toán", // Đánh dấu trạng thái ban đầu là 'Pending'
                BookingId = model.OrderId // Dùng OrderId từ model
            };


            try
            {
                await _unitOfWork.PaymentRepository.CreateAsync(payment);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    Message = "Có lỗi xảy ra khi lưu thông tin thanh toán.",
                    Error = dbEx.InnerException?.Message ?? dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Có lỗi xảy ra khi lưu thông tin thanh toán.",
                    Error = ex.InnerException?.Message ?? ex.Message
                });
            }

            // Tạo URL thanh toán thông qua VNPay
            var paymentUrl = _vpnpayService.CreatePaymentUrl(HttpContext, model);

            return Ok(new { PaymentUrl = paymentUrl, PaymentId = payment.Id });
        }
        [HttpGet("PaymentCallBack")]
        public async Task<IActionResult> PaymentCallBack()
        {
            var response = _vpnpayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponsecode != "00")
            {
                // Chuyển hướng đến trang lỗi thanh toán với thông báo
                string FailUrl = $"http://localhost:5173?message={Uri.EscapeDataString($"Thanh toán thành công: {response.VnPayResponsecode}")}";
                return Redirect(FailUrl);
            }



            var payment = await _unitOfWork.PaymentRepository.GetByBookingIdAsync(response.OrderId);
            if (payment == null)
            {
                return NotFound("Không tìm thấy đơn hàng thanh toán.");
            }

            // Cập nhật trạng thái khi thanh toán thành công
            payment.Status = "Đã thanh toán";
            await _unitOfWork.PaymentRepository.UpdateAsync(payment);

            // Chuyển hướng đến trang thanh toán thành công
            string successUrl = $"https://localhost:7166/swagger/index.html?message={Uri.EscapeDataString($"Thanh toán thành công: {response.VnPayResponsecode}")}";
            return Redirect(successUrl);

        }
        






    }

}













