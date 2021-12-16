using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  BloomLog.Api.Dtos;
using  BloomLog.Api.Entities;
using  BloomLog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloomLog.Api.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostMemRepository repository;
        private readonly ILogger<PostsController> logger;  

        public PostsController(IPostMemRepository repository, ILogger<PostsController> logger)
        {
            this.repository = repository;
            this.logger = logger;
          
        }

        [HttpGet]
        public async Task<IEnumerable<PostsDto>> GetPostsAsync()
        {
            var posts = (await repository.GetPostsAsync())
                        .Select(post => post.AsDto());



            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {posts.Count()} posts");

            return posts;
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostsDto>> GetPostAsync(Guid id)
        {
            var post = await repository.GetPostAsync(id);

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
                Category = PostsDto.Category,
                Content = PostsDto.Content,
                Dated = DateTimeOffset.UtcNow
            };

            await repository.CreatePostsAsync(post);

            return CreatedAtAction(nameof(GetPostAsync), new { id = post.Id }, post.AsDto());
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePostsAsync(Guid id, UpdatePostDto PostsDto)
        {
            var existingPosts = await repository.GetPostAsync(id);

            if (existingPosts is null)
            {
                return NotFound();
            }

            existingPosts.Title = PostsDto.Title;
            existingPosts.Content= PostsDto.Content;

     

            await repository.UpdatePostsAsync(existingPosts);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostsAsync(Guid id)
        {
            var existingPosts = await repository.GetPostAsync(id);

            if (existingPosts is null)
            {
                return NotFound();
            }

            await repository.DeletePostsAsync(id);

            return NoContent();
        }
    }
}