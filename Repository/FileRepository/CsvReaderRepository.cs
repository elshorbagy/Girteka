using System.Text;

namespace Repository.FileRepository
{
    public class CsvReaderRepository : ICsvReaderRepository
    {
        readonly string _path;

        public CsvReaderRepository()
        {
            _path = Path.Combine(Directory
                .GetParent(Directory.GetCurrentDirectory())
                .FullName, @"Repository\Data\");
        }

        public async Task<IEnumerable<string>> Read(string filename)
        {
            var filePath = _path + filename;
            return await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
        }
    }
}
