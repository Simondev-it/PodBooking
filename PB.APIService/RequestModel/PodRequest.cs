namespace PB.APIService.RequestModel
{
    public class PodRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? Description { get; set; }

        public int? Rating { get; set; }

        public string? Status { get; set; }

        public int TypeId { get; set; }

        public int StoreId { get; set; }

    }
}
