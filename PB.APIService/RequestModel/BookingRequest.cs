using PodBooking.SWP391.Models;

namespace PB.APIService.RequestModel
{
    public class BookingRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime? CurrentDate { get; set; }

        public string Status { get; set; }

        public string Feedback { get; set; }
        public int? Rating { get; set; }

        public int PodId { get; set; }

        public int UserId { get; set; }

        public virtual ICollection<int> SlotIds { get; set; } = new List<int>();
    }
}
