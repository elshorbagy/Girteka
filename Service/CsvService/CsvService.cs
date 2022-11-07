using Domain.Models;
using Microsoft.Extensions.Options;
using Repository.FileRepository;
using Repository.SQLRepository;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;

namespace Service.CsvService
{
    public class CsvService : ICsvService
    {
        readonly ICsvReaderRepository _csvFileReaderRepository;
        readonly CsvConfiguration? _configuration;
        readonly ISQLRepository _sqlRepository;
        readonly ILogger<CsvService> _logger;

        readonly ConcurrentBag<Electricity> _electricityList = new();
        readonly ConcurrentBag<Region> _regionList = new();
        readonly string _className;

        public CsvService(
            ICsvReaderRepository csvReaderRepository,
            ISQLRepository sqlRepository,
            IOptions<CsvConfiguration> configuration,
            ILogger<CsvService> logger)
        {
            _configuration = configuration.Value;

            _className = nameof(CsvService);

            _csvFileReaderRepository = csvReaderRepository;
            _sqlRepository = sqlRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<VwElectricityDatum>> Get(string pavadinimas, int top, int skip)
        {
            try
            {
                var query = await _sqlRepository.Get(pavadinimas);
                return query.Skip(skip).Take(top);
            }
            catch(Exception exception)
            {
                _logger.LogError($"{_className} threw an exception:{exception.Message} in Get Function");
                throw;
            }
        }

        public async Task<bool> Save(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<string> lines = await GetCSVData();

                MapCsvData(lines);

                var saveRegionsResult = await _sqlRepository.AddBulkRegionAsync(_regionList, cancellationToken);
                var saveElectricityResult = await _sqlRepository.AddBulkElectricityAsync(_electricityList, cancellationToken);

                return saveRegionsResult && saveElectricityResult;
            }
            catch (Exception exception)
            {
                _logger.LogError($"{_className} threw an exception:{exception.Message} in Save Function");
                throw;
            }
        }

        private async Task<IEnumerable<string>> GetCSVData()
        {
            var data = Enumerable.Empty<string>();

            foreach (var file in _configuration.Files)
            {
                IEnumerable<string> lines = await _csvFileReaderRepository.Read(file, true);
                data = data.Concat(lines);
            }
            return data;
        }

        private void MapCsvData(IEnumerable<string> lines)
        {
            var options = new ParallelOptions { MaxDegreeOfParallelism = _configuration
                .MaxDegreeOfParallelism
                .Value
            };

            int index = 1;

            _ = Parallel.ForEach(lines, options, line =>
            {
                var values = line.Split(_configuration.Delimiter);
                lock (_regionList) {
                    AddRegion(_regionList, values[0], Interlocked.Increment(ref index));

                var electricityData = new Electricity
                {
                    Pavadinimas = !string.IsNullOrEmpty(values[1]) ? values[1] : null,
                    RegionId = _regionList.FirstOrDefault(x => x.RegionName == values[0]).RegionId,
                    Tipas = !string.IsNullOrEmpty(values[2]) ? values[2] : null,
                    Numeris = !string.IsNullOrEmpty(values[3]) ? int.Parse(values[3]) : null,
                    Ppliusas = !string.IsNullOrEmpty(values[4]) ? decimal.Parse(values[4]) : null,
                    Plt = !string.IsNullOrEmpty(values[5]) ? DateTime.Parse(values[5]) : null,
                    Pminusas = !string.IsNullOrEmpty(values[6]) ? decimal.Parse(values[6]) : null
                };

                    _electricityList.Add(electricityData);
                }
            });
        }

        private static void AddRegion(ConcurrentBag<Region> regions, string regionName, int index)
        {
            if (regions.FirstOrDefault(x => x.RegionName.Trim() == regionName.Trim()) == null)
            {
                var newRegion = new Region
                {
                    RegionName = regionName,
                    RegionId = index
                };
                regions.Add(newRegion);
            }
        }
    }
}
