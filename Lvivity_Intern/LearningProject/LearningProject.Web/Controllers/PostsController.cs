using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<List<Post>> GetPosts([FromBody]PaginationModel input)
        {
            return await _postService.GetPosts(input.Take, input.Skip);
        }

        [HttpPost]
        public async Task<Post> CreatePost([FromForm]CreatePostModel createPostModel)
        {
            var post = await _postService.CreatePost(createPostModel);

            if (post == null)
            {
                throw new Exception("Post not created!");
            }

            return post;
        }
    }
}