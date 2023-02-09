using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;
using WPSUR.WebApi.Models.Chat.Request;
using WPSUR.WebApi.Models.Chat.Response;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("getChat")]
        public async Task<ActionResult<ChatResponse>> GetChat([FromQuery]GetChatRequest chatRequest)
        {
            try
            {
                if (chatRequest == null)
                {
                    return BadRequest("Error occured, try again later.");
                }

                GetChatServiceRequest serviceRequest = new()
                {
                    UserFromId = chatRequest.UserFrom,
                    UserToId = chatRequest.UserTo,
                };

                Chat serviceResponse = await _service.GetChat(serviceRequest);

                ChatResponse controllerResponse = new()
                {
                    Messages = serviceResponse.Messages,
                    UserFrom = chatRequest.UserFrom,
                    UserTo = chatRequest.UserTo,
                };

                return Ok(controllerResponse);
            }
            catch(Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            } 
        }

        [HttpGet("getChats")]
        public async Task<ActionResult<ICollection<Guid>>> GetChats([FromQuery]Guid senderId)
        {
            try
            {
                ICollection<Guid> Interlocutors = await _service.GetInterlocutors(senderId);

                return Ok(Interlocutors);
            } 
            catch (Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }
    }
}
