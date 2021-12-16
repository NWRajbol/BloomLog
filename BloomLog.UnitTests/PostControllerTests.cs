using System;
using System.Timers;
using System.Threading.Tasks;
using BloomLog.Api.Controllers;
using BloomLog.Api.Dtos;
using BloomLog.Api.Entities;
using BloomLog.Api.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace BloomLog.UnitTests
{
    public class PostControllerTests
    {
        private readonly Mock<IPostMemRepository> repositoryStub = new();
        private readonly Mock<ILogger<PostsController>> loggerStub = new();

        private readonly Random rand = new();

        [Fact]
        public async Task GetPostsAsync_WithUnexistingPosts_ReturnsNotFound()
        {
            // Arrange

            repositoryStub.Setup(repo => repo.GetPostAsync(It.IsAny<Guid>()))
                    .ReturnsAsync((Posts)null);

            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);

            //Act

            var result = await controller.GetPostAsync(Guid.NewGuid());


            //Assert

        
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        

        [Fact]
         public async Task GetPostsAsync_WithUnexistingPosts_ReturnsExpectedPost()
        {
            //Arrange 

            var expectedPost = CreateRandomPost();

            repositoryStub.Setup(repo => repo.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedPost);

            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);

            //Act

            var result = await controller.GetPostAsync(Guid.NewGuid());

            //Assert

            result.Value.Should().BeEquivalentTo(
                expectedPost);
           

        }


        [Fact]
         public async Task GetPostsAsync_WithUnexistingPosts_ReturnsAllPosts()
        {
            // Arrange

            var expectedPosts = new[] {CreateRandomPost(), CreateRandomPost(), CreateRandomPost()};

            repositoryStub.Setup(repo => repo.GetPostsAsync()).ReturnsAsync(expectedPosts);

            
            
            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);

            //Act

            var actualPosts = await controller.GetPostsAsync();

            // Assert

            actualPosts.Should().BeEquivalentTo(
                expectedPosts);
            

        }

 /*
        [Fact]
         public async Task GetPostsAsync_WithMatchingCategory_ReturnsMatchingPosts()
        {
            // Arrange

            var allPosts = new[] {
                new Posts(){Category = "Technology"},
                new Posts(){Category = "Science"},
                new Posts(){Category = "Health"}

            };

            var categoryToMatch ="Health";

            repositoryStub.Setup(repo => repo.GetPostsAsync()).ReturnsAsync(allPosts);
            
            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);

            //Act

            IEnumerable<PostsDto> foundPosts = await controller.GetPostsAsync(categoryToMatch);

            // Assert

            foundPosts.Should().OnlyContain(
                posts => posts.Category == allPosts[2].Category );
            

        }
 */

        
        [Fact]
         public async Task CreatePostsAsync_WithPostToCreate_ReturnsCreatedPost()
        {
            // Arrange
           
            var postToCreate = new CreatePostDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), rand.Next(1000).ToString());

            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);

            //Act

            var result = await controller.CreatePostsAsync(postToCreate);

            // Assert
            var createdPost = (result.Result as CreatedAtActionResult).Value as PostsDto;
            postToCreate.Should().BeEquivalentTo(
                createdPost,
                options => options.ComparingByMembers<PostsDto>().ExcludingMissingMembers()
            );

            createdPost.Id.Should().NotBeEmpty();
            createdPost.Dated.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));

        }



        [Fact]
         public async Task UpdatePostAsync_WithExistingPost_ReturnsNoContent()
        {
            // Arrange
            var existingPost = CreateRandomPost();

            repositoryStub.Setup(repo => repo.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPost);


            var postId = existingPost.Id;
            var postToUpdate = new UpdatePostDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), rand.Next(1000).ToString());

            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);


            //Act

            var result = await controller.UpdatePostsAsync(postId, postToUpdate);  

            // Assert
            result.Should().BeOfType<NoContentResult>();

        }


        [Fact]
         public async Task DeletePostAsync_WithExistingPost_ReturnsNoContent()
        {
            // Arrange
            var existingPost = CreateRandomPost();

            repositoryStub.Setup(repo => repo.GetPostAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingPost);


            
            var controller = new PostsController(repositoryStub.Object, loggerStub.Object);


            //Act

            var result = await controller.DeletePostsAsync(existingPost.Id);  

            // Assert
            result.Should().BeOfType<NoContentResult>();

        }

        private Posts CreateRandomPost(){
            return new(){
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                Content = rand.Next(1000).ToString(),
                Dated = DateTimeOffset.UtcNow
            };
        }
    }
}