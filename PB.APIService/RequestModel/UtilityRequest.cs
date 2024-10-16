namespace PB.APIService.RequestModel
{
        public class UtilityRequest
        {
            public int Id { get; set; }

            public string? Name { get; set; }

            public string? Image { get; set; }

            public string? Description { get; set; }

            public List<PodRequest> Pods { get; set; } = new List<PodRequest>();
    }
    }
