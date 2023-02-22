using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Widgets;
using WPSUR.WebApi.Models.Widgets;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupantNumbersController : ApiControllerBase
    {
        private readonly IOccupantNumbersService _numbersService;
        public OccupantNumbersController(IOccupantNumbersService numbersService)
        {
            _numbersService = numbersService ?? throw new ArgumentNullException(nameof(numbersService));
        }

        [HttpGet("OccupantNumbers")]
        public async Task<ActionResult<KilledRussiansResponse>> GetOccupantNumbers()
        {
            try
            {
                var freshNumbers = await _numbersService.GetFreshNumbers();

                KilledRussiansResponse response = new()
                {
                    TotalKilled = freshNumbers.TotalKilled,
                    DailyKilled = freshNumbers.DailyKilled,
                    Identifier = freshNumbers.Identifier,
                };
                return response;
            }
            catch (NullReferenceException)
            {
                KilledRussiansModel unknownModel = new()
                {
                    TotalKilled = "Hundred of thousands",
                    DailyKilled = "Hundreds",
                    Identifier = "Unknown forms of life",
                };
                return Ok(unknownModel);
            }
        }
    }
}
