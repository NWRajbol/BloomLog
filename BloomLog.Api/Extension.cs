using  BloomLog.Api.Dtos;
using  BloomLog.Api.Entities;

namespace BloomLog.Api
{
    public static class Extension
    {
        public static PostsDto AsDto(this Posts posts)
        {
            return new PostsDto(posts.Id, posts.Title, posts.Category, posts.Content, posts.Dated);
               
        }
    }
}
