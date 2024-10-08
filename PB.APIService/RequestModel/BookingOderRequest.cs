namespace PB.APIService.RequestModel
{
    public class BookingOderRequest
    {
        public int Id { get; set; }

        public int? Amount { get; set; }

        public int? Quantity { get; set; }

        public string? Status { get; set; }

        public DateTime? Date { get; set; }

        public int BookingId { get; set; }

        public int ProductId { get; set; }
    }
}
