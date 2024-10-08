namespace PB.APIService.RequestModel
{
    public class SlotRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? StartTime { get; set; }

        public int? EndTime { get; set; }

        public int? Price { get; set; }

        public string? Status { get; set; }

        public int PodId { get; set; }
    }
}
