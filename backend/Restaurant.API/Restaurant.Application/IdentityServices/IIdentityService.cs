using Restaurant.Application.Models;
using Restaurant.Application.Models.Identities;
using Restaurant.Application.Models.Identities.JWTSettings;

namespace Restaurant.Application.IdentityServices
{
    public interface IIdentityService
    {
        public Task<TokenModel> Login(Login loginDto);

        public Task<Response> Register(Register registerDto);

        public Task<TokenModel> RefreshAccessToken(UserDto userDto);

        public Task<Response> LockOutUser(int userId);

        public Task<Response> UnLockOutUser(int userId);

        public Task<bool> IsUserExists(string username);

        public Task<UserDto> UpdateUser(UserDto userDto);

        public Task<TokenModel> ChooseMainRole(MainRole mainRole);

        public Task<Response> ChangeUserPassword(ChangePassword changePassword);

        public Task<Response> AddRolesToUser(int userId, List<string> roles, UserDto userDto);
        public Task<string> GetMainRole(int mainRoleId);
    }
}
