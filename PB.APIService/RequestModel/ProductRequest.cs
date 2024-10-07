namespace PB.APIService.RequestModel
{
    public class ProductRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int Stock { get; set; }

        public int StoreId { get; set; }

        public int CategoryId { get; set; }
    }
}
