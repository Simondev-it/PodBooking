using PB.APIService.RequestModel;

namespace PB.APIService.Services
{
    public interface IVnpayService
    {
        string CreatePaymentUrl(HttpContext context , VnPaymentRequestModel model );
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
