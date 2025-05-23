using AutoMapper;
using BlogAPI.Data;
using BlogAPI.DTOs.Categories;
using BlogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            ApplicationDbContext context, 
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                if (categoryDtos == null)
                {
                    _logger.LogError("Falha ao mapear as categorias para DTOs.");
                    return Enumerable.Empty<CategoryDto>();
                }

                return categoryDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os categorias.");
                return Enumerable.Empty<CategoryDto>();
            }
        }
    }
} 