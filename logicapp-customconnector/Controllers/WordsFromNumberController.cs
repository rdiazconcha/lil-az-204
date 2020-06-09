using Humanizer;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace logicapp_customconnector.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsFromNumberController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetWordsFromNumber(int value = 0)
        {
            return Ok(value.ToWords(CultureInfo.CreateSpecificCulture("es-mx")));
        }
    }
}