using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using  BloomLog.Api.Entities;


namespace BloomLog.Api.Repositories{
    public interface IPostMemRepository
        {
            Task CreatePostsAsync(Posts posts);
            Task DeletePostsAsync(Guid id);
            Task<Posts> GetPostAsync(Guid id);
            Task<IEnumerable<Posts>> GetPostsAsync();
            Task UpdatePostsAsync(Posts posts);

            
    }
}