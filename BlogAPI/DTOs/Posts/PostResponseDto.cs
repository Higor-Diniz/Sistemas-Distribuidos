namespace BlogAPI.DTOs.Posts
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CategoryName { get; set; }
        public string AuthorUsername { get; set; }
    }
} 