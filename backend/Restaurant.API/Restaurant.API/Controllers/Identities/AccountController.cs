using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Attributes;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.Identities.JWTSettings;
using Restaurant.Application.Services.IdentityServices;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.API.Controllers.Identities;
public class AccountController : BaseController
{
    private readonly IIdentityService _identityService;
    public AccountController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<Response> Register([FromBody] Register register)
        => await _identityService.Register(register);

    [HttpPost("Login"), AllowAnonymous]
    public async Task<TokenModel> Login([FromBody] Login login)
        => await _identityService.Login(login);

    [HttpPost("RefreshAccessToken"), AllowAnonymous]
    public async Task<TokenModel> RefreshAccessToken()
        => await _identityService.RefreshAccessToken(UserDto);

    [HttpGet("LockOutUser/{userId}"), AuthorizedRoles(Roles.Manager)]
    public async Task<Response> LockOutUser(int userId)
        => await _identityService.LockOutUser(userId);

    [HttpGet("UnLockOutUser/{userId}"), AuthorizedRoles(Roles.Manager)]
    public async Task<Response> UnLockOutUser(int userId)
        => await _identityService.UnLockOutUser(userId);

    [HttpGet("IsThisUserNameExist"), AllowAnonymous]
    public async Task<bool> IsThisUserNameExist([FromQuery] string username)
        => await _identityService.IsUserExists(username);

    [HttpPost("UpdateUser")]
    public async Task<UserDto> UpdateUser()
        => await _identityService.UpdateUser(UserDto);

    [HttpPost("ChooseMainRole")]
    [Authorize]
    public async Task<TokenModel> ChooseMainRole([FromBody] MainRole mainRole)
        => await _identityService.ChooseMainRole(mainRole);

    [HttpPost("ChangePassword")]
    public async Task<Response> ChangePassword([FromBody] ChangePassword changePassword)
        => await _identityService.ChangeUserPassword(changePassword);

    [HttpGet("UserProfile")]
    public UserDto UserProfile()
        => UserDto;

    [HttpGet("Test")]
    public IActionResult Test()
        => Ok("");

    [HttpGet("GetMainRole")]
    [AuthorizedRoles(Roles.Manager, Roles.Suplier, Roles.Student)]
    public async Task<string> GetMainRoleById(int roleId)
        => await _identityService.GetMainRole(roleId);
}
