using System;
namespace BloomLog.Dtos
{
 public record PostsDto
    {
            public Guid Id{get; init;}
            public string Title{ get; init;}
            public string Content{get; init;}
            public DateTimeOffset Dated{get; init;}
    }    
}