namespace BloomLog.Entities{
    public record Posts
    {
            public Guid Id{get; init;}
            public string Title{ get; init;}
            public string Content{get; init;}
            public DateTimeOffset Dated{get; init;}
    }
}