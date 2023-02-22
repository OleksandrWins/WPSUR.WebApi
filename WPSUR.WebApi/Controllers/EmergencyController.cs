using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Emergency;
using WPSUR.WebApi.Models.Emergency.Request;
using WPSUR.WebApi.Models.Emergency.Response;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyController : ApiControllerBase
    {
        private readonly IEmergencyService _service;

        public EmergencyController(IEmergencyService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<EmergencyInfoResponse>> GetEmergencyInfo()
        {
            try
            {
                EmergencyInfo emergencyInfo = await _service.GetEmergencyInfoAsync(LoggedInUserId);
                EmergencyInfoResponse response = new()
                {
                    EmergencyContent = emergencyInfo.EmergencyContent,
                    EmergencyList = emergencyInfo.EmergencyList
                };

                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [Authorize]
        [HttpPost("SetEmergencyInfo")]
        public async Task<ActionResult> SetEmergencyInfo([FromBody] EmergencyRequest emergencyRequest)
        {
            try
            {
                EmergencyInfo emergencyInfo = new()
                {
                    EmergencyContent = emergencyRequest.EmergencyMessage,
                    EmergencyList = emergencyRequest.EmergencyEmails
                };
                await _service.SetEmergencyInfoAsync(LoggedInUserId, emergencyInfo);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [Authorize]
        [HttpPost("EmergencyCall")]
        public async Task<ActionResult> EmergencyCall()
        {
            try
            {
                await _service.EmergencyCallAsync(LoggedInUserId);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

    }
}
