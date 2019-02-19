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
    public class PostController:ControllerBase
    {
        private readonly IPostService _objUserService;

        public PostController(IPostService userService)
        {
            _objUserService = userService;
        }

        [HttpPost]
        public async Task<List<Post>> GetPost([FromBody]PaginationModel paginationModel)
        {
            var getposts = await _objUserService.GetPosts(paginationModel);

            return getposts;
        }
    }
}