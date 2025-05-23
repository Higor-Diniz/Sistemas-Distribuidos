using AutoMapper;
using BlogAPI.DTOs.Categories;
using BlogAPI.DTOs.Posts;
using BlogAPI.Entities;

namespace BlogAPI.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeamento do retorno da tabela 'posts'
            CreateMap<Post, PostResponseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AuthorUsername, opt => opt.MapFrom(src => src.User.Username));

            // Mapeamento o objeto de transferência: Post
            CreateMap<PostDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            // Mapeamento do objeto de transferência: categories
            CreateMap<Category, CategoryDto>();
        }
    }
} 