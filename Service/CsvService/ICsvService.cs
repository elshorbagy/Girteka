using Domain.Models;

namespace Service.CsvService
{
    public interface ICsvService
    {
        Task<IEnumerable<VwElectricityDatum>> Get(string pavadinimas, int top, int skip);
        Task<bool> Save(CancellationToken cancellationToken);
    }
}
