using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BlogAPI.DTOs.Posts;
using BlogAPI.Services.Interfaces;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetPosts([FromQuery] int? categoryId)
        {
            var posts = await _postService.GetPostsAsync(categoryId);

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseDto>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult<PostResponseDto>> CreatePost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var userId = int.Parse(User.FindFirst("uid")?.Value);
            var result = await _postService.CreatePostAsync(postDto, 1);
            
            return CreatedAtAction(nameof(GetPost), new { id = result.Id }, result);
        }

        // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // var userId = int.Parse(User.FindFirst("uid")?.Value);
            var result = await _postService.UpdatePostAsync(id, postDto, 1);
            
            if (!result.Success)
                return result.Message == "Não encontrado!" ? NotFound() : Forbid();

            return NoContent();
        }

        // [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            // var userId = int.Parse(User.FindFirst("uid")?.Value);
            var result = await _postService.DeletePostAsync(id, 1);
            
            if (!result.Success)
                return result.Message == "Não encontrado!" ? NotFound() : Forbid();

            return NoContent();
        }
    }
} 