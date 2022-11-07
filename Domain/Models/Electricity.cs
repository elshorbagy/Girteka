namespace Domain.Models
{
    public class Electricity
    {
        public long? Id { get; set; }
        public long? RegionId { get; set; }
        public string Pavadinimas { get; set; } = null!;
        public string Tipas { get; set; } = null!;
        public long? Numeris { get; set; }
        public decimal? Ppliusas { get; set; }
        public DateTime? Plt { get; set; }
        public decimal? Pminusas { get; set; }
    }
}
