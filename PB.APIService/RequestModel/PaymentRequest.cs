namespace PB.APIService.RequestModel
{
    public class PaymentRequest
    {
        public int Id { get; set; }

        public string Method { get; set; }

        public int Amount { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public int BookingId { get; set; }
    }
}
