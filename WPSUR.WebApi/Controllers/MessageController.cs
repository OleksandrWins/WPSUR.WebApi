using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Exceptions.UserExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Messages;
using WPSUR.Services.Models.Messages.Requests;
using WPSUR.WebApi.Models.Message.Request;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ApiControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [Authorize]
        [HttpPut("updateMessage")]
        public async Task<IActionResult> UpdateMessage(UpdateMessageRequest messageRequest)
        {
            try
            {
                if (messageRequest == null)
                {
                    return BadRequest("Input data is equal to null.");
                }

                MessageToUpdate messageToUpdate = new()
                {
                    Content = messageRequest.Content,
                    Id = messageRequest.Id,
                };

                await _service.UpdateAsync(messageToUpdate);

                return Ok();
            }
            catch (MessageDoesNotExistException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (MessageValidationException messageException)
            {
                return BadRequest(messageException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }

        [Authorize]
        [HttpDelete("deleteMessages")]
        public async Task<IActionResult> DeleteMessages([FromBody] ICollection<Guid> Ids)
        {
            try
            {
                if (!Ids.Any())
                {
                    return BadRequest("Input messages to delete is equal to null.");
                }

                await _service.DeleteMessagesAsync(Ids);

                return Ok();
            }
            catch (UserDoesNotExistException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (MessageDoesNotExistException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (MessageValidationException messageException)
            {
                return BadRequest(messageException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }

        [Authorize]
        [HttpPost("createMessage")]
        public async Task<ActionResult<Guid>> SendMessage(CreateMessageRequest createMessageRequest)
        {
            try
            {
                if (createMessageRequest == null)
                {
                    return BadRequest("Input data is equal to null");
                }

                ChatMessage messageToService = new()
                {
                    Content = createMessageRequest.Content,
                    UserFrom = LoggedInUserId,
                    UserTo = createMessageRequest.UserTo,
                };

                Guid messageId = await _service.CreateAsync(messageToService);

                return Ok(messageId);
            }
            catch(MessageValidationException messageException)
            {
                return BadRequest(messageException.Message);
            }
            catch (Exception)
            {
                return BadRequest("Unexpected exception. Try again.");
            }
        }
    }
}
