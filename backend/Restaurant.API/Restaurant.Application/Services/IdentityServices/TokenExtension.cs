using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.Identities.JWTSettings;
using Restaurant.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurant.Application.Services.IdentityServices;

public static class TokenExtension
{
    private static List<Claim> _claims { get; set; } = new List<Claim>();

    public static async Task<TokenModel> GetToken(
        ApplicationUser applicationUser,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        JWTSetting jWTSetting)
    {
        AddClaims(applicationUser);
        await AddRolesToClaim(userManager, roleManager, applicationUser);

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSetting?.JWT?.Secret!));

        var token = new JwtSecurityToken(
            issuer: jWTSetting!.JWT.ValidIssuer,
            audience: jWTSetting.JWT.ValidAudience,
            expires: DateTime.Now.AddMinutes(40),
            claims: _claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        return new TokenModel
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };
    }

    private static void AddClaims(ApplicationUser user)
    {
        _claims = new List<Claim>()
        {
            new (ClaimTypes.Name, user.UserName!),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new ("FullName", user.FullName),
            new ("MainRoleId", user.MainRoleId?.ToString()!),
            new (ClaimTypes.Email, user.Email!),
            new ("Id",user.Id.ToString()),
        };
    }

    private static async Task AddRolesToClaim(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationUser user)
    {
        var userRoles = await userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            _claims.Add(new Claim("AllRoles", role));
        }

        if (user.MainRoleId == null && userRoles.Count > 0)
        {
            var mainRoleStr = userRoles.FirstOrDefault() ?? string.Empty;

            var mainRole = await roleManager.FindByNameAsync(mainRoleStr);

            user.MainRoleId = mainRole!.Id;

            if (mainRoleStr != null)
                _claims.Add(new Claim(ClaimTypes.Role, mainRoleStr));
        }
        else
        {
            var mainRole = await roleManager.FindByIdAsync(user.MainRoleId.ToString()!);
            _claims.Add(new Claim(ClaimTypes.Role, mainRole!.Name!));
        }

        user.UpdateActive();
        await userManager.UpdateAsync(user);
    }


    public static UserDto GetUserDto(ClaimsPrincipal model)
    {
        if (model == null)
            return new UserDto();

        var claims = model.Claims;

        var user = new UserDto()
        {
            Id = int.Parse(claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "0"),
            FullName = claims.FirstOrDefault(x => x.Type == "FullName")?.Value ?? string.Empty,
            MainRoleId = int.Parse(claims.FirstOrDefault(x => x.Type == "MainRoleId")?.Value!),
            Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty,
            Roles = claims.Where(x => x.Type == "AllRoles").Select(x => x.Value).ToArray(),
            UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty
        };

        return user;
    }
}
