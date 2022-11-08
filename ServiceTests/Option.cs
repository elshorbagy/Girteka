using Domain.Models;
using Microsoft.Extensions.Options;

namespace Tests
{
    public static class Option
    {
        public static IOptions<CsvConfiguration> SetOptions()
        {
            return Options.Create(new CsvConfiguration()
            {
                Delimiter = "/",
                MaxDegreeOfParallelism = 3,
                Files = new List<string>()
            });
        }
    }
}
