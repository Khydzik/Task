using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [Authorize]
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
            var posts =  await _postService.GetPosts(input.Take, input.Skip);
            if (posts == null) { throw new Exception("No posts."); }
            return posts;
        }

       
    }
}