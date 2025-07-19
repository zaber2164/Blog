using Microsoft.AspNetCore.Identity;

namespace Blog.Models
{
    public class AppUser : IdentityUser
    {
        // Add custom properties here
        public string? FullName { get; set; }
        public string? Bio { get; set; }
    }
}
