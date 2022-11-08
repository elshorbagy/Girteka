namespace Domain.Models
{
    public class Electricity
    {
        public int? Id { get; set; }
        public int? RegionId { get; set; }
        public string Pavadinimas { get; set; } = null!;
        public string Tipas { get; set; } = null!;
        public int? Numeris { get; set; }
        public decimal? Ppliusas { get; set; }
        public DateTime? Plt { get; set; }
        public decimal? Pminusas { get; set; }
    }
}
