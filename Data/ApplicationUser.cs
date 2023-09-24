using Microsoft.AspNetCore.Identity;

namespace myproject.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}