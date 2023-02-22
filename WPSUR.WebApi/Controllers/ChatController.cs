using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Exceptions.UserExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;
using WPSUR.WebApi.Models.Chat.Response;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ApiControllerBase
    {
        private readonly IChatService _service;

        public ChatController(IChatService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize]
        [HttpGet("findChat")]
        public async Task<ActionResult<ICollection<ChatReceiverResponse>>> FindChat([FromQuery] string email)
        {
            try
            {
                ICollection<ChatReceiverResponse> interlocutorsViewResponse = (await _service.FindChats(email, LoggedInUserId)).Select(chat => new ChatReceiverResponse
                {
                    ReceiverEmail = chat.UserEmail,
                    ReceiverFirstName = chat.UserToFirstName,
                    ReceiverLastName = chat.UserToLastName,
                    ReceiverId = chat.UserId,
                }).ToList();

                return Ok(interlocutorsViewResponse);
            }
            catch(EmailValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }

        [Authorize]
        [HttpGet("getChat")]
        public async Task<ActionResult<ChatResponse>> GetChat([FromQuery]Guid userTo)
        {
            try
            {
                GetChatServiceRequest serviceRequest = new()
                {
                    UserFromId = LoggedInUserId,
                    UserToId = userTo,
                };

                Chat serviceResponse = await _service.GetChat(serviceRequest);

                ChatResponse controllerResponse = new()
                {
                    Messages = serviceResponse.Messages,
                    Sender = serviceResponse.Sender,
                    Receiver = serviceResponse.Receiver,
                };

                return Ok(controllerResponse);
            }
            catch(Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            } 
        }

        [Authorize]
        [HttpGet("getChats")]
        public async Task<ActionResult<ICollection<ChatsResponse>>> GetChats()
        {
            try
            {
                Guid senderId = LoggedInUserId;

                ICollection<ChatsResponse> interlocutors = await _service.GetInterlocutors(senderId);

                List<ChatReceiverResponse> response = interlocutors.Select(interlocutor => new ChatReceiverResponse
                {
                    ReceiverId = interlocutor.UserId, 
                    ReceiverFirstName = interlocutor.UserToFirstName, 
                    ReceiverLastName = interlocutor.UserToLastName, 
                    ReceiverEmail = interlocutor.UserEmail, 
                }).ToList();

                return Ok(response);
            } 
            catch (Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }
    }
}
