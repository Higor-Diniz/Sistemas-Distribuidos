using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs.Posts
{
    public class PostDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        public string Tag { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
} 