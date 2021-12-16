using System;
using System.ComponentModel.DataAnnotations;

namespace BloomLog.Api.Dtos{
    public record PostsDto(Guid Id,string Title, string Category, string Content, DateTimeOffset Dated);

    public  record CreatePostDto([Required] string Title, string Category, string Content);
    public  record UpdatePostDto([Required] string Title, string Category, string Content);


}