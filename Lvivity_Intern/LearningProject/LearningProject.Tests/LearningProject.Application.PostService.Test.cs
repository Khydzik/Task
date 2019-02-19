using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using Moq;
using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Http;
using LearningProject.Data;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectApplicationPostServiceTest
    {
        [Fact]
        public void GetPostsCheckReturnPosts_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 2 };

            var mock = new Mock<IPostDataStore>();

            mock.Setup(repo => repo.GetPosts(paginationModel)).Returns(GetTestPosts());

            var postService = new PostService(mock.Object);

            var result = postService.GetPosts(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public async Task CreateNewPostCheckIsNullNewPost_Test()
        {
            CreatePostModel newPostModel = null;
            var mock = new Mock<IPostDataStore>();

            var postService = new PostService(mock.Object);

            Exception ex = await Assert.ThrowsAsync<NullReferenceException>(() => postService.CreatePost(newPostModel));

            Assert.Equal("Null File", ex.Message);
        }

        [Fact]
        public async Task CreateNewPostCheckIsNullFile_Test()
        {
            CreatePostModel createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = null
            };
            var mock = new Mock<IPostDataStore>();

            var postService = new PostService(mock.Object);

            Exception ex = await Assert.ThrowsAsync<NullReferenceException>(() => postService.CreatePost(createPostModel));

            Assert.Equal("Empty File", ex.Message);
        }

        [Fact]
        public void CreateNewPostCheckReturnTrue_Test()
        {
            var mock = new Mock<IPostDataStore>();
            var fileMock = new Mock<IFormFile>();
            var fileName = "i.jpeg";

            Post postModel = new Post
            {
                Id = 4,
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                ImageUrl = "i.jpeg"
            };

            CreatePostModel createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object
            };
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            mock.Setup(repo => repo.IsSavePost(It.IsAny<Post>())).Returns(async() => { return true; });
            
            var webRootPath = "C:\\Task\\Lvivity_Intern\\WebApi\\LearningProject\\wwwroot";
            
            var postService = new PostService(mock.Object, Mock.Of<IHostingEnvironment>(w => w.WebRootPath == webRootPath));

            var result = postService.CreatePost(createPostModel);

            Assert.True(result.Result);
        }

        [Fact]
        public async Task CreateNewPostCheckIsTypeOfFile_Test()
        {
            var fileMock = new Mock<IFormFile>();
            var fileName = "i.txt";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            CreatePostModel createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object
            };
            var mock = new Mock<IPostDataStore>();

            var postService = new PostService(mock.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => postService.CreatePost(createPostModel));

            Assert.Equal("Invalid file type.", ex.Message);
        }

        private async Task<List<Post>> GetTestPosts()
        {
            var posts = new List<Post>
            {
                new Post { Id=1, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"},
                new Post { Id=2, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"}

            };
            return posts;
        }
    }
}
