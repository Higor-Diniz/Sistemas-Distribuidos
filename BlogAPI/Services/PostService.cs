using AutoMapper;
using BlogAPI.Data;
using BlogAPI.DTOs.Posts;
using BlogAPI.Entities;
using BlogAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(
            ApplicationDbContext context, 
            IMapper mapper,
            ILogger<PostService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PostResponseDto>> GetPostsAsync(int? categoryId)
        {
            try
            {
                var query = _context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .AsQueryable();

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                var posts = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
                
                var postDtos = _mapper.Map<IEnumerable<PostResponseDto>>(posts);
                if (postDtos == null)
                {
                    _logger.LogError("Falha ao mapear as postagens para DTOs.");
                    return Enumerable.Empty<PostResponseDto>();
                }
                
                return postDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter os posts.");
                return Enumerable.Empty<PostResponseDto>();
            }
        }

        public async Task<PostResponseDto> GetPostByIdAsync(int id)
        {
            try
            {
                var post = await _context.Posts
                    .Include(p => p.Category)
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (post == null)
                    return null;

                var postDto = _mapper.Map<PostResponseDto>(post);
                if (postDto == null)
                {
                    _logger.LogError($"Falha ao mapear o post com ID ({id}) para DTO.");
                    return null;
                }

                return postDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter o post com o ID ({id}).");
                return null;
            }
        }

        public async Task<PostResponseDto> CreatePostAsync(PostDto postDto, int userId)
        {
            try
            {
                var post = _mapper.Map<Post>(postDto);
                if (post == null)
                {
                    _logger.LogError("Falha ao mapear o objeto 'PostDto' para o objeto 'Post'.");
                    return null;
                }

                post.UserId = userId;
                post.CreatedAt = DateTime.UtcNow;
                post.UpdatedAt = DateTime.UtcNow;

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return await GetPostByIdAsync(post.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar post.");
                return null;
            }
        }

        public async Task<ServiceResponse<bool>> UpdatePostAsync(int id, PostDto postDto, int userId)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                    return ServiceResponse<bool>.FailureResponse("Post não encontrado!");

                if (post.UserId != userId)
                    return ServiceResponse<bool>.FailureResponse("Usuário não autorizado!");

                try
                {
                    _mapper.Map(postDto, post);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Falha ao mapear o objeto 'PostDto' para o objeto 'Post' durante a atualização.");
                    return ServiceResponse<bool>.FailureResponse("Falha ao atualizar o post");
                }

                post.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return ServiceResponse<bool>.SuccessResponse(true, "Post atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o post com ID ({id}).");
                return ServiceResponse<bool>.FailureResponse($"A atualização falhou: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<bool>> DeletePostAsync(int id, int userId)
        {
            try
            {
                var post = await _context.Posts.FindAsync(id);

                if (post == null)
                    return ServiceResponse<bool>.FailureResponse("Post não encontrado!");

                if (post.UserId != userId)
                    return ServiceResponse<bool>.FailureResponse("Usuário não autorizado!");

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return ServiceResponse<bool>.SuccessResponse(true, "Post excluído com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir o post com ID ({id}).");
                return ServiceResponse<bool>.FailureResponse($"Exclusão falhou: {ex.Message}.");
            }
        }
    }
} 