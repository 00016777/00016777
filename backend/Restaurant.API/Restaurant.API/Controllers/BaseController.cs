using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.IdentityServices;
using Restaurant.Application.Models.Identities;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UserDto UserDto => TokenExtension.GetUserDto(User);
    }
}
