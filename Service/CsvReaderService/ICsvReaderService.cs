namespace Service.CsvReaderService
{
    public interface ICsvReaderService
    {
        Task<bool> Read(string filterWord);
    }
}
