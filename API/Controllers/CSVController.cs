using Domain.Models;
using Girteka_Ahmed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.FileRepository;
using Service.CsvReaderService;
using System.Collections.Concurrent;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        readonly ICsvReaderService _csvReaderService;

        public CSVController(ICsvReaderService csvReaderService)
        {
            _csvReaderService = csvReaderService;
        }

        [HttpGet("{filterWord}")]
        //public async Task<IActionResult> Get()
        public IActionResult Get(string filterWord)
        {
            var data = _csvReaderService.Read(filterWord);
            return Ok();
        }
    }
}
