using Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.DBContext;
using Repository.FileRepository;
using Repository.SQLRepository;
using Tests;

namespace Service.CsvService.Tests
{
    [TestClass()]
    public class CsvServiceTests
    {
        readonly SQLDBContext _dbContext;

        readonly Mock<ICsvReaderRepository> _mockCsvReaderRepository;
        readonly SQLRepository _sqlRepository;
        readonly Mock<ILogger<CsvService>> _logger;

        public CsvServiceTests()
        {
            _mockCsvReaderRepository = new Mock<ICsvReaderRepository>();
            
            _logger = new Mock<ILogger<CsvService>>();

            _dbContext = DataSetupTest.CreateDBContext();

            AddData();

            _sqlRepository = new SQLRepository(_dbContext);
        }

        void AddData()
        {
            _dbContext.Electricities.Add(DataSetupTest.SetElectricity());
            _dbContext.Regions.Add(DataSetupTest.SetRegion());
            _dbContext.SaveChanges();
        }

        [TestMethod()]
        public async Task GetTest()
        {
            var csvService = IntializeObject();
            var result = await csvService.Get(string.Empty, 1, 1);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GetTest_Fails()
        {
            var csvService = IntializeObjectFail();
            Assert.ThrowsException<AggregateException>(() => csvService.Get(string.Empty, 1, 1).Result);
        }

        [TestMethod]
        public async Task Save()
        {
            var csvService = IntializeObject();
            var result = await csvService.Save(new CancellationToken());
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void Save_Fails()
        {
            var csvService = IntializeObjectFail();
            Assert.ThrowsException<AggregateException>(() => csvService.Save(new CancellationToken()).Result);
        }

        CsvService IntializeObject()
        {
            var options = Options.Create(new CsvConfiguration()
            {
                Files = new List<string>()
            });
            return new CsvService(_mockCsvReaderRepository.Object, _sqlRepository, options, _logger.Object);
        }

        CsvService IntializeObjectFail()
        {
            var options = Options.Create(new CsvConfiguration());
            return new CsvService(_mockCsvReaderRepository.Object, null, options, _logger.Object);
        }
    }
}