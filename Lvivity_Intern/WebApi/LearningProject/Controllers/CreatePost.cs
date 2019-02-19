using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreatePostController:Controller
    {
        private readonly IPostService _objUserService;

        public CreatePostController(IPostService userService)
        {
            _objUserService = userService;
        }

        [HttpPost]
        public async Task<bool> CreatePost([FromForm]CreatePostModel createPostModel)
        {
            var createPost = await _objUserService.IsCreateNewPost(createPostModel);

            return createPost;
        }
    }
}
