namespace Domain.Models
{
    public class CsvConfiguration
    {
        public IEnumerable<string>? Files { get; set; }
        public string? Delimiter { get; set; }
        public int? MaxDegreeOfParallelism { get; set; }
    }
}
