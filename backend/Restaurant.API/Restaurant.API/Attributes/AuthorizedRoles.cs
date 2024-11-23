using Microsoft.AspNetCore.Authorization;

namespace Restaurant.API.Attributes
{
    public class AuthorizedRolesAttribute : AuthorizeAttribute
    {
        public AuthorizedRolesAttribute(params string[] roles)
        {
            Roles = string.Join(',', roles);
        }
    }
}
