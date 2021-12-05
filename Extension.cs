using BloomLog.Dtos;
using BloomLog.Entities;

namespace BloomLog
{
    public static class Extension
    {
        public static PostsDto AsDto(this Posts posts)
        {
            return new PostsDto{
               Id = posts.Id,
               Title = posts.Title,
               Content = posts.Content,
               Dated = posts.Dated
        };
    }
    }
}