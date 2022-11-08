using API.Controllers;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.FileRepository;
using Repository.SQLRepository;
using Service.CsvService;

namespace Tests.Controllers
{
    [TestClass()]
    public class CSVControllerTests
    {
        readonly Mock<ICsvReaderRepository> _mockCsvReaderRepository;
        readonly Mock<ISQLRepository> _mockSqlRepository;
        readonly Mock<ILogger<CsvService>> _logger;

        public CSVControllerTests()
        {
            _mockCsvReaderRepository = new Mock<ICsvReaderRepository>();
            _mockSqlRepository = new Mock<ISQLRepository>();
            _logger = new Mock<ILogger<CsvService>>();
        }

        [TestMethod()]
        public async Task Get()
        {
            var csvController = IntializeObject();
            var result = (await csvController.Get()) as OkObjectResult;
            Assert.AreEqual(result?.StatusCode, 200);
        }

        [TestMethod()]
        public async Task Get_Fails()
        {
            var csvController = IntializeObjectNull();
            var result = (await csvController.Get()) as BadRequestObjectResult;
            Assert.AreEqual(result?.StatusCode, 400);
        }

        [TestMethod()]
        public async Task Post_Fails()
        {
            var csvController = IntializeObjectNull();
            var result = (await csvController.Post(new CancellationToken())) as BadRequestObjectResult;
            Assert.AreEqual(result?.StatusCode, 400);
        }

        [TestMethod()]
        public async Task Post()
        {
            var csvController = IntializeObject();
            var result = (await csvController.Post(new CancellationToken())) as OkObjectResult;
            Assert.AreEqual(result?.StatusCode, 200);
        }

        CSVController IntializeObject()
        {
            IOptions<CsvConfiguration> options = Option.SetOptions();
            var csvService = new CsvService(_mockCsvReaderRepository.Object, _mockSqlRepository.Object, options, _logger.Object);
            return new CSVController(csvService);
        }

        static CSVController IntializeObjectNull()
        {
            return new CSVController(null);
        }
    }
}
