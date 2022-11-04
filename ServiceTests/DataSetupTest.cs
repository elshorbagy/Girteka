using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Domain.Models;

namespace Tests
{
    public static class DataSetupTest
    {
        public static SQLDBContext CreateDBContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder<SQLDBContext>()
                .UseInMemoryDatabase(databaseName: "Girteka_Ahmed")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            return new SQLDBContext((DbContextOptions<SQLDBContext>)options);
        }

        public static Electricity SetElectricity()
        {
            return new Electricity()
            {
                Id = 1,
                Numeris = 222,
                Pavadinimas ="Butas",
                Plt = DateTime.Now,
                Pminusas = 3,
                Ppliusas = 0,
                RegionId = 2,
                Tipas = "Test"
            };
        }

        public static Region SetRegion()
        {
            return new Region()
            {
                RegionId = 2,
                RegionName = "Test Region"
            };
        }

        public static VwElectricityDatum SetVwElectricityDatum()
        {
            return new VwElectricityDatum()
            {
                RegionName = "Test Region",
                Tipas = "Test",
                Ppliusas = 2,
                Pminusas = 3,
                Numeris = 4,
                Pavadinimas = "Butas",
                Plt = DateTime.Now
            };
        }
    }
}
