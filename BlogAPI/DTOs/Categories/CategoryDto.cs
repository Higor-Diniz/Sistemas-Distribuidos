using System.ComponentModel.DataAnnotations;

namespace BlogAPI.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [StringLength(300)]
        public string Description { get; set; }
    }
} 