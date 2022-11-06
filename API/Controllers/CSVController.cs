using Domain.Models;
using Girteka_Ahmed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.CsvService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        readonly ICsvService _csvReaderService;

        public CSVController(ICsvService csvReaderService)
        {
            _csvReaderService = csvReaderService;
        }

        [HttpGet("{pavadinimas}/{skip}/{top}")]
        public async Task<IActionResult> Get(string pavadinimas = "Butas", int skip = 0, int top = 100)
        {
            try
            {
                return Ok(await _csvReaderService.Get(pavadinimas, top, skip));
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post(CancellationToken cancellationToken)
        {
            try
            {
                var data = await _csvReaderService.Save(cancellationToken);
                return Ok(data);
            }
            catch (Exception exception)
        {
                return BadRequest(exception.Message);
            }
        }
    }
}
