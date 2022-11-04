namespace Repository.FileRepository
{
    public interface ICsvReaderRepository
    {
        IEnumerable<string> Read(string path);
    }
}
