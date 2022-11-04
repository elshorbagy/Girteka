namespace Domain.Models
{
    public class CsvData
    {
        //TINKLAS,OBT_PAVADINIMAS,OBJ_GV_TIPAS,OBJ_NUMERIS,P+,PL_T,P-

        public string Tinklas { get; set; }

        public string? Vienetas { get; set; }

        public string? Tipas { get; set; }

        public int Numeris { get; set; }

        public double? Ppliusas { get; set; }

        public DateTime? PLT { get; set; }

        public double? PMinusas { get; set; }

        public string? Letter { get; set; }
    }
}
