using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Attributes;
using Restaurant.Application.IdentityServices;
using Restaurant.Application.Models.Identities;
using Restaurant.Domain.Entities.Identity;

namespace Restaurant.API.Controllers;
public class AccountController : BaseController
{
    private readonly IIdentityService _identityService;
    public AccountController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("Register"), AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] Register register)
        => Ok(await _identityService.Register(register, UserDto));

    [HttpPost("Login"), AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] Login login)
        => Ok(await _identityService.Login(login));

    [HttpPost("RefreshAccessToken"), AllowAnonymous]
    public async Task<IActionResult> RefreshAccessToken()
        => Ok(await _identityService.RefreshAccessToken(UserDto));

    [HttpGet("LockOutUser/{userId}"), AuthorizedRoles(Roles.Admin)]
    public async Task<IActionResult> LockOutUser(int userId)
        => Ok(await _identityService.LockOutUser(userId));

    [HttpGet("UnLockOutUser/{userId}"), AuthorizedRoles(Roles.Admin)]
    public async Task<IActionResult> UnLockOutUser(int userId)
        => Ok(await _identityService.UnLockOutUser(userId));

    [HttpGet("IsThisUserNameExist"), AllowAnonymous]
    public async Task<IActionResult> IsThisUserNameExist(string username)
        => Ok(await _identityService.IsUserExists(username));

    [HttpPost("UpdateUser")]
    public async Task<IActionResult> UpdateUser()
        => Ok(await _identityService.UpdateUser(UserDto));

    [HttpPost("ChooseMainRole"), AuthorizedRoles(Roles.Admin)]
    public async Task<IActionResult> ChooseMainRole([FromBody] MainRole mainRole)
        => Ok(await _identityService.ChooseMainRole(mainRole));

    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        => Ok(await _identityService.ChangeUserPassword(changePassword));
}
