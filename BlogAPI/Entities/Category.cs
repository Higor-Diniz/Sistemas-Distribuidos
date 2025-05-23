using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [MaxLength(300)]
        public string Description { get; set; }
        
        public ICollection<Post> Posts { get; set; }
    }
} 