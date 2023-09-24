using System.ComponentModel.DataAnnotations;

namespace myproject.Models
{
    public class SignUpDto
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Password{ get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword{ get; set; }
    }
}