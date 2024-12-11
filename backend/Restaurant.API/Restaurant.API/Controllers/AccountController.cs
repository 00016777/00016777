using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Attributes;
using Restaurant.Application.IdentityServices;
using Restaurant.Application.Models;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.Identities.JWTSettings;
using Restaurant.Domain.Entities.Identity;
using System.Reflection.Metadata.Ecma335;

namespace Restaurant.API.Controllers;
public class AccountController : BaseController
{
    private readonly IIdentityService _identityService;
    public AccountController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<Response> Register([FromBody] Register register)
        => await _identityService.Register(register, UserDto);

    [HttpPost("Login"), AllowAnonymous]
    public async Task<TokenModel> Login([FromBody] Login login)
        => await _identityService.Login(login);

    [HttpPost("RefreshAccessToken"), AllowAnonymous]
    public async Task<TokenModel> RefreshAccessToken()
        => await _identityService.RefreshAccessToken(UserDto);

    [HttpGet("LockOutUser/{userId}"), AuthorizedRoles(Roles.Admin)]
    public async Task<Response> LockOutUser(int userId)
        => await _identityService.LockOutUser(userId);

    [HttpGet("UnLockOutUser/{userId}"), AuthorizedRoles(Roles.Admin)]
    public async Task<Response> UnLockOutUser(int userId)
        => await _identityService.UnLockOutUser(userId);

    [HttpGet("IsThisUserNameExist"), AllowAnonymous]
    public async Task<bool> IsThisUserNameExist([FromQuery] string username)
        => await _identityService.IsUserExists(username);

    [HttpPost("UpdateUser")]
    public async Task<UserDto> UpdateUser()
        => await _identityService.UpdateUser(UserDto);

    [HttpPost("ChooseMainRole"), AuthorizedRoles(Roles.Admin)]
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
}
