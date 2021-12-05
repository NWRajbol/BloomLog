using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomLog.Dtos;
using BloomLog.Entities;
using BloomLog.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloomLog.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostMemRepository repository;
       

        public PostsController(IPostMemRepository repository)
        {
            this.repository = repository;
          
        }

        [HttpGet]
        public async Task<IEnumerable<PostsDto>> GetPostsAsync()
        {
            var posts = (await repository.GetPostsAsync())
                        .Select(post => post.AsDto());

            return posts;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PostsDto>> GetPostsAsync(Guid id)
        {
            var post = await repository.GetPostsAsync(id);

            if (post is null)
            {
                return NotFound();
            }

            return post.AsDto();
        }

      
        [HttpPost]
        public async Task<ActionResult<PostsDto>> CreatePostsAsync(CreatePostDto PostsDto)
        {
            Posts post = new()
            {
                Id = Guid.NewGuid(),
                Title = PostsDto.Title,
                Content = PostsDto.Content,
                Dated = DateTimeOffset.UtcNow
            };

            await repository.CreatePostsAsync(post);

            return CreatedAtAction(nameof(GetPostsAsync), new { id = post.Id }, post.AsDto());
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePostsAsync(Guid id, UpdatePostDto PostsDto)
        {
            var existingPosts = await repository.GetPostsAsync(id);

            if (existingPosts is null)
            {
                return NotFound();
            }

            Posts updatedPost = existingPosts with {
                Title = PostsDto.Title,
                Content = PostsDto.Content
            };

            await repository.UpdatePostsAsync(updatedPost);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostsAsync(Guid id)
        {
            var existingPosts = await repository.GetPostsAsync(id);

            if (existingPosts is null)
            {
                return NotFound();
            }

            await repository.DeletePostsAsync(id);

            return NoContent();
        }
    }
}