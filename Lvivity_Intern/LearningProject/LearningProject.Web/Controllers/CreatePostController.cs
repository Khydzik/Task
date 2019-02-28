using LearningProject.Application;
using LearningProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LearningProject.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CreatePostController:ControllerBase
    {
        private readonly IPostService _postService;

        public CreatePostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<Post> CreatePost([FromForm]CreatePostModel input)
        {
            byte[] imageData = null;
            var binaryReader = new BinaryReader(input.Image.OpenReadStream());
            imageData = binaryReader.ReadBytes((int)input.Image.Length);

            var post = await _postService.CreatePost(input.Title, input.ShortDescription, input.Image.FileName,imageData);

            if (post == null)
            {
                throw new Exception("Post not created!");
            }
            return post;
        }
    }
}
