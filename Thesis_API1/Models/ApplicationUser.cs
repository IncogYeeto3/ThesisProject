using Microsoft.AspNetCore.Identity;

namespace Thesis_API1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Number { get; set; }
        public string Role { get; set; }
    }
}
