using BlogAPI.DTOs.Categories;

namespace BlogAPI.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
    }
} 