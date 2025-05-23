using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public string Role { get; set; } = "User";
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public ICollection<Post> Posts { get; set; }
    }
} 