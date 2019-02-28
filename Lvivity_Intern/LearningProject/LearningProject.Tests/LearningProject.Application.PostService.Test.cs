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
using System.Linq;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectApplicationPostServiceTest
    {
        //[Fact]
        //public async Task GetPostsCheckReturnPosts_Test()
        //{
        //    int Skip = 1;
        //    int Take = 2;

        //    var posts = new List<Post>
        //    {
        //        new Post { Id=1, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
        //        "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"},
        //        new Post { Id=2, Title="Luxury interior in the design of apartments and houses", ShortDescription ="Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
        //        "in favor of a unique environment that best meets the needs of the customer.", ImageUrl = "i.jpeg"}
        //    };

        //    IQueryable<Post> postsQuery = posts.AsQueryable();

        //    var mock1 = new Mock<IRepository<Post>>();
        //    var mock2 = new Mock<IHostingEnvironment>();
            
        //    mock1.Setup(repo => repo.Query()).Returns(async() => {
        //      return await posts.Skip(Skip - 1).Take(Take).ToListAsync(postsQuery, new System.Threading.CancellationToken());
        //    });

        //    var postService = new PostService(mock1.Object,mock2.Object);

        //    var result = postService.GetPosts(Skip,Take);

        //    Assert.Equal(2, result.Result.Count);
        //}

        [Fact]
        public void CreateNewPostCheckReturnPost_Test()
        {
            var mock1 = new Mock<IRepository<Post>>();
            var mock2 = new Mock<IHostingEnvironment>();
            var fileMock = new Mock<IFormFile>();
            var fileName = "i.jpeg";
            Post postModel = new Post
            {
                Id = 4,
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                ImageUrl = "i.jpeg"
            };
            

            byte[] imageData = new byte[255];

            fileMock.Setup(_ => _.FileName).Returns(fileName);

            mock1.Setup(repo => repo.InsertAsync(It.IsAny<Post>())).Returns(async() => { return postModel; });
            
            var webRootPath = "C:\\Task\\Lvivity_Intern\\WebApi\\LearningProject\\wwwroot";
            
            var postService = new PostService(mock1.Object, Mock.Of<IHostingEnvironment>(w => w.WebRootPath == webRootPath));

            var result = postService.CreatePost(postModel.Title,
                postModel.ShortDescription, postModel.ImageUrl, imageData);

            Assert.IsType<Post>(result.Result);
        }

        [Fact]
        public async Task CreateNewPostCheckIsTypeOfFile_Test()
        {
            var mock1 = new Mock<IRepository<Post>>();
            var mock2 = new Mock<IHostingEnvironment>();
            var fileMock = new Mock<IFormFile>();
            var fileName = "i.txt";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            CreatePostModel createPostModel = new CreatePostModel
            {
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning.",
                Image = fileMock.Object
            };

            byte[] imageData = null;

            var postService = new PostService(mock1.Object,mock2.Object);

            Exception ex = await Assert.ThrowsAsync<Exception>(() => postService.CreatePost(createPostModel.Title,
                createPostModel.ShortDescription,createPostModel.Image.FileName, imageData));

            Assert.Equal("Invalid file type.", ex.Message);
        }
    }
}
