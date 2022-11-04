using System.Text;

namespace Repository.FileRepository
{
    public class CsvReaderRepository : ICsvReaderRepository
    {
        readonly string _path;

        public CsvReaderRepository()
        {
            _path = Path.Combine(Environment.CurrentDirectory, @"Data\");
        }

        public IEnumerable<string> Read(string filename)
        {
            var filePath = _path + filename;
            return File.ReadLines(filePath, Encoding.UTF8);
        }
    }
}
