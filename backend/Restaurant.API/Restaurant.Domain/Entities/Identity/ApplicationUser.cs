using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Commons;
using System.Reflection.Metadata.Ecma335;

namespace Restaurant.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime LastActive { get; set; }

        public int CountOfEnter { get; set; }

        public int? MainRoleId { get; set; }

        public int? MainPositionId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public void UpdateActive()
        {
            LastActive = DateTime.Now;
            CountOfEnter += 1;
        }
    }
}
