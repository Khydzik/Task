using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;
using LearningProject.Data.Models;
using LearningProject.Data;
using Microsoft.EntityFrameworkCore;

namespace LearningProject.Tests
{
    [ExcludeFromCodeCoverage]
    public class LearningProjectDataPostServiceTest
    {
        private readonly PostDataStore _postDataStore;

        public LearningProjectDataPostServiceTest()
        { 
            _postDataStore = DataStoreConfig();
        }

        [Fact]
        public void GetListPost_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 2 };

            var result = _postDataStore.GetPosts(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public void GetListPostMoreItemsThenAre_Test()
        {
            var paginationModel = new PaginationModel { CurrentPage = 1, PerPage = 100 };

            var result = _postDataStore.GetPosts(paginationModel);

            Assert.Equal(2, result.Result.Count);
        }

        [Fact]
        public void SavePost_Test()
        {
            Post post = new Post
            {
                Id = 1,
                Title = "Luxury interior in the design of apartments and houses",
                ShortDescription = "Designing premium interiors is a rejection of typical planning, finishing and decoration decisions " +
                "in favor of a unique environment that best meets the needs of the customer.",
                ImageUrl = "i.jpeg"
            };
            
            var result = _postDataStore.IsSavePost(post);

            Assert.True(result.Result);
        }

        private PostDataStore DataStoreConfig()
        {
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "ReturnRole_From_Database")
                .Options;

            var context = new DataBaseContext(options);

            var postDataStore = new PostDataStore(context);

            return postDataStore;
        }

    }
}
