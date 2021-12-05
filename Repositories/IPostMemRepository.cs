using System.Collections.Generic;
using BloomLog.Entities;


namespace BloomLog.Repositories{
    public interface IPostMemRepository
        {
            Task CreatePostsAsync(Posts posts);
            Task DeletePostsAsync(Guid id);
            Task<Posts> GetPostsAsync(Guid id);
            Task<IEnumerable<Posts>> GetPostsAsync();
            Task UpdatePostsAsync(Posts posts);
    }
}