using Domain.Models;

namespace Repository.SQLRepository
{
    public interface ISQLRepository
    {
        Task<IEnumerable<VwElectricityDatum>> Get(string pavadinimas);
        Task<bool> AddBulkRegionAsync(IEnumerable<Region> regions, CancellationToken cancellationToken);
        Task<bool> AddBulkElectricityAsync(IEnumerable<Electricity> electricities, CancellationToken cancellationToken);
    }
}
