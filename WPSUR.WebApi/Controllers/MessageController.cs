using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Exceptions.UserExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Messages;
using WPSUR.WebApi.Models.Message.Request;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

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

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageRequest createMessageRequest)
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
                    UserFrom = createMessageRequest.UserFrom,
                    UserTo = createMessageRequest.UserTo,
                };

                await _service.CreateAsync(messageToService);

                return Ok();
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
