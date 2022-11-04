namespace Repository.FileRepository
{
    public interface ICsvReaderRepository
    {
        Task<IEnumerable<string>> Read(string path);
    }
}
