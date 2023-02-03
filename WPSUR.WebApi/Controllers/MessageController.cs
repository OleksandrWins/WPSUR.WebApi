using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models;
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

                await _service.CreateMessageAsync(messageToService);

                return Ok();
            }
            catch (NullReferenceException nullException)
            {
                return BadRequest(nullException.Message);
            }
            catch(MessageValidationException messageException)
            {
                return BadRequest(messageException.Message);
            }
            catch(ArgumentNullException argumentNullException)
            {
                return BadRequest(argumentNullException.Message); 
            }
            catch (DbException)
            {
                return BadRequest("An error occured while saving data to database");
            }
            catch (Exception)
            {
                return BadRequest("Unhandled exception occurred while processing message creation.");
            }
        }
    }
}
