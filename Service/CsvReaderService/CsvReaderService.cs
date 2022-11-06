using Domain.Models;
using Microsoft.Extensions.Options;
using Repository.FileRepository;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CsvReaderService
{
    public class CsvReaderService : ICsvReaderService
    {
        readonly ICsvReaderRepository _csvReaderRepository;
        readonly Files? _fileList;

        public CsvReaderService(ICsvReaderRepository csvReaderRepository, IOptions<Files> filesList)
        {
            _fileList = filesList.Value;

            _csvReaderRepository = csvReaderRepository;
        }

        public Task<bool> Read(string filterWord)
        {
            IEnumerable<string> lines = GetCSVData(filterWord);

            var options = new ParallelOptions { MaxDegreeOfParallelism = 2 };

            var data = new ConcurrentBag<CsvData>();

            Parallel.ForEach(lines, options, line =>
            {

                var y = line.Split(',')[0];
                var data1 = new CsvData
                {
                    Tinklas = y
                };

                data.Add(data1);

            });

            return null;
        }

        private IEnumerable<string> GetCSVData(string filterWord)
        {
            var data = Enumerable.Empty<string>();

            foreach (var file in _fileList.Locations)
            {
                _ = data.Concat(_csvReaderRepository.Read(file)
                                .Where(x => x.Contains(filterWord)));
            }
             return data;
        }
    }
}
