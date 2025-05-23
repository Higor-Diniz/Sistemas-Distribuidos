using BlogAPI.DTOs.Posts;

namespace BlogAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseDto>> GetPostsAsync(int? categoryId);
        Task<PostResponseDto> GetPostByIdAsync(int id);
        Task<PostResponseDto> CreatePostAsync(PostDto postDto, int userId);
        Task<ServiceResponse<bool>> UpdatePostAsync(int id, PostDto postDto, int userId);
        Task<ServiceResponse<bool>> DeletePostAsync(int id, int userId);
    }
} 