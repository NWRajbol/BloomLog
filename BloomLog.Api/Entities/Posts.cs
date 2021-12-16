using System;

namespace BloomLog.Api.Entities{
    public class Posts
    {
            public Guid Id{get; set;}
            public string Title{ get; set;}

            public string Category { get; set;}
            public string Content{get; set;}
            public DateTimeOffset Dated{get; set;}
    }
}