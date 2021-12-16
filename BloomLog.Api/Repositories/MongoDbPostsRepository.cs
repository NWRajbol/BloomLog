using  BloomLog.Api.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloomLog.Api.Repositories{
    public class MongoDbPostsRepository : IPostMemRepository
    {
        private const string databaseName = "bloomlog";
        private const string collectionName = "posts";
        private readonly IMongoCollection<Posts> postsCollection;
        private readonly FilterDefinitionBuilder<Posts> filterBuilder = Builders<Posts>.Filter;


        public MongoDbPostsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            postsCollection = database.GetCollection<Posts>(collectionName);
        }

        public async Task CreatePostsAsync(Posts posts)
        {
           await postsCollection.InsertOneAsync(posts);
        }

        public async Task DeletePostsAsync(Guid id)
        {
           var filter = filterBuilder.Eq(post => post.Id, id);
           await postsCollection.DeleteOneAsync(filter);
        }

       
        
        public async Task<Posts> GetPostAsync(Guid id)
        {
            var filter = filterBuilder.Eq(post => post.Id, id);
            return await postsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Posts>> GetPostsAsync()
        {
            return await postsCollection.Find(new BsonDocument()).ToListAsync();
        }


        public async Task UpdatePostsAsync(Posts posts)
        {
            var filter = filterBuilder.Eq(post => post.Id, posts.Id);
            await postsCollection.ReplaceOneAsync(filter, posts);
        }
    }
}