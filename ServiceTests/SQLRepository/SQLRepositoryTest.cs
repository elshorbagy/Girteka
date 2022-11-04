using Repository.DBContext;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests;
using System.Collections.Generic;
using Service.CsvService;

namespace Repository.SQLRepository.Tests
{
    [TestClass()]
    public class SQLRepositoryTest
    {
        readonly SQLDBContext _dbContext;
        readonly SQLRepository _sqlRepository;
        readonly SQLRepository _sqlRepositoryFail;

        public SQLRepositoryTest()
        {
            _dbContext = DataSetupTest.CreateDBContext();

            AddData();

            _sqlRepository = new SQLRepository(_dbContext);
            _sqlRepositoryFail = new SQLRepository(null);
        }

        void AddData()
        {
            _dbContext.Electricities.Add(DataSetupTest.SetElectricity());
            _dbContext.Regions.Add(DataSetupTest.SetRegion());
            _dbContext.SaveChanges();
        }

        [TestMethod()]
        public async Task AddBulkElectricityAsync()
        {
            var list = new List<Electricity>
            {
                DataSetupTest.SetElectricity()
            };
            var result = await _sqlRepository.AddBulkElectricityAsync(list, new CancellationToken());

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AddBulkElectricityAsync_Fails()
        {
            Assert.ThrowsException<AggregateException>(() =>
            _sqlRepositoryFail.AddBulkElectricityAsync(new List<Electricity>(), new CancellationToken()).Result);
        }

        [TestMethod()]
        public async Task AddBulkRegionAsync()
        {
            var list = new List<Region>
            {
                DataSetupTest.SetRegion()
            };
            var result = await _sqlRepository.AddBulkRegionAsync(list, new CancellationToken());

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AddBulkRegionAsync_Fails()
        {
            Assert.ThrowsException<AggregateException>(() =>
            _sqlRepositoryFail.AddBulkRegionAsync(new List<Region>(), new CancellationToken()).Result);
        }

        [TestMethod()]
        public async Task Get()
        {
            var result = await _sqlRepository.Get("Butas");
            Assert.IsTrue(result.Any());
        }

        [TestMethod()]
        public void Get_Fails()
        {
            Assert.ThrowsException<AggregateException>(() =>
            _sqlRepositoryFail.Get("").Result);
        }
    }
}
