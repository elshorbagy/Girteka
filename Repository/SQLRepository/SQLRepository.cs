using Domain.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;

namespace Repository.SQLRepository
{
    public class SQLRepository : ISQLRepository
    {
        private readonly SQLDBContext _sqlDbContext;

        public SQLRepository(SQLDBContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<bool> AddBulkElectricityAsync(IEnumerable<Electricity> electricities, CancellationToken cancellationToken)
        {
            var transaction = await _sqlDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _sqlDbContext.BulkInsertAsync(electricities.ToList(), cancellationToken: cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> AddBulkRegionAsync(IEnumerable<Region> regions, CancellationToken cancellationToken)
        {
            var transaction = await _sqlDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _sqlDbContext.AddRangeAsync(regions, cancellationToken);
                await _sqlDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<VwElectricityDatum>> Get(string pavadinimas)
        {
            try
            {
                return await Task.FromResult((
                    from p in _sqlDbContext.Electricities.Where(x => x.Pavadinimas == pavadinimas.Trim())
                    from r in _sqlDbContext.Regions.Where(c => c.RegionId == p.RegionId)
                    select new { p.Id, p.Tipas, p.Pavadinimas, p.Plt, p.Pminusas, p.Numeris, p.Ppliusas, r.RegionName }).Select(x =>
                    new VwElectricityDatum
                    {
                        Numeris = x.Numeris,
                        Pavadinimas = x.Pavadinimas,
                        Ppliusas = x.Ppliusas,
                        Plt = x.Plt,
                        Pminusas = x.Pminusas,
                        Tipas = x.Tipas,
                        RegionName = x.RegionName
                    })
                    .AsSplitQuery()
                    .AsNoTracking()
                    );
            }
            catch
            {
                throw;
            }
        }
    }
}
