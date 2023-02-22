using Microsoft.AspNetCore.Mvc;
using WPSUR.Services.Exceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;
using WPSUR.WebApi.Models.Account.Request;
using Microsoft.AspNetCore.Authorization;
using WPSUR.WebApi.Constants;

namespace WPSUR.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, IAuthService authService, ITokenService tokenService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequest userData)
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
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest userData)
        {
            try
            {
                LoginUser loginUser = new() { Email = userData.Email, Password = userData.Password };
                User user = await _authService.LoginAsync(loginUser);
                var token = _tokenService.GetToken(user);

                return Ok(token);
            }
            catch (InvalidPasswordException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
            }
        }

        [Authorize(Roles = Identity.AdminRole)]
        [HttpGet("TestAuthentication")]
        public ActionResult<string> TestAuthentication()
            => Ok(LoggedInUserId);
    }
}
