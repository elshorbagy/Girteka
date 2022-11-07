using System.Text;

namespace Repository.FileRepository
{
    public class CsvReaderRepository : ICsvReaderRepository
    {
        readonly string _path;

        public CsvReaderRepository()
        {
            _path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Data/");         
        }

        public async Task<IEnumerable<string>> Read(string filename, bool hasHeader)
        {
            var filePath = _path + filename;
            var lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
            return hasHeader ? lines.Skip(1) : lines;
        }
    }
}
