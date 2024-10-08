namespace PB.APIService.RequestModel
{
    public class BookingRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }

        public string Feedback { get; set; }

        public int PodId { get; set; }

        public int UserId { get; set; }
    }
}
