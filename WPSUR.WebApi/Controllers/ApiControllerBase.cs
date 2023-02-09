using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WPSUR.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected internal Guid LoggedInUserId
        {
            get => new Guid(User.FindFirstValue(ClaimTypes.UserData));
        }
    }
}
