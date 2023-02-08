using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Exceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;
using WPSUR.WebApi.Models.Account.Request;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IPasswordHashService _passwordHashService;

        public AccountController(IAccountService accountService, IPasswordHashService passwordHashService)
        {
            _accountService = accountService;
            _passwordHashService = passwordHashService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterRequest userData)
        {
            try
            {
                RegisterUser registerUser = new()
                {
                    Email = userData.Email,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Password = userData.Password,
                };

                await _accountService.RegisterAsync(registerUser);
                return Ok();
            }
            catch (NoCredentialsException exception) 
            {
                return BadRequest(exception.Message);
            }
            catch (UserExistsException exception) 
            {
                return BadRequest(exception.Message);
            }
            catch (PasswordStrengthException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
            }
        }

        [HttpPost("Login")]
        public void Login([FromBody] LoginRequest userData)
        {
            var user = new LoginUser() { Email = userData.Email, Password = userData.Password };
            try
            {
                  
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
