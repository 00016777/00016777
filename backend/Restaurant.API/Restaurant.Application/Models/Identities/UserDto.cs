using Microsoft.AspNetCore.Http;

namespace Restaurant.Application.Models.Identities
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; } 
        public int? MainRoleId { get; set; }
        public string[] Roles { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

    }
}
