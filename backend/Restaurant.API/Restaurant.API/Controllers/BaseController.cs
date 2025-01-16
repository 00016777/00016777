using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Services.IdentityServices;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected UserDto UserDto => TokenExtension.GetUserDto(User);
    }
}
