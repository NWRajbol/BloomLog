using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloomLog.Entities;

namespace BloomLog.Repositories
{
    public class PostMemRepository : IPostMemRepository{
        private readonly List<Posts> ppost = new()
        {
            new Posts { Id = Guid.NewGuid(), Title = "Potion", Content = "adsfa", Dated = DateTimeOffset.UtcNow },
            new Posts { Id = Guid.NewGuid(), Title = "Iron Sword", Content = "dfasdf", Dated = DateTimeOffset.UtcNow },
            new Posts { Id = Guid.NewGuid(), Title = "Bronze Shield", Content = "asdfasdf", Dated = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            return await Task.FromResult(ppost);
        }

        public async Task<Posts> GetPostsAsync(Guid id)
        {
            var post = ppost.Where(posts => posts.Id == id).SingleOrDefault();
            return await Task.FromResult(post);
        }

        public async Task CreatePostsAsync(Posts post)
        {
            ppost.Add(post);
            await Task.CompletedTask;
        }

        public async Task UpdatePostsAsync(Posts post)
        {
            var index = ppost.FindIndex(existingPosts => existingPosts.Id == post.Id);
            ppost[index] = post;
            await Task.CompletedTask;
        }

        public async Task DeletePostsAsync(Guid id)
        {
            var index = ppost.FindIndex(existingPosts => existingPosts.Id == id);
            ppost.RemoveAt(index);
            await Task.CompletedTask;
        }
        }
    }
