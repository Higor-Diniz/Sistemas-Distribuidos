using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs.Auth
{
    public class UserRegistrationDto
    {
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
        
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
} 