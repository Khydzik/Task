using LearningProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateNewPostController: ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IHostingEnvironment _appHosting;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        public CreateNewPostController(ApplicationContext context, IHostingEnvironment app)
        {            
            _context = context;
            _appHosting = app;
        }        

        [HttpPost]
        public async Task<bool> CreatePost([FromForm]CreatePostModel createPostModel)
        {
            if (createPostModel == null) throw new Exception("Null File");

            if (createPostModel.Image.Length == 0) throw new Exception("Empty File");

            if (createPostModel.Image.Length > 10 * 1024 * 1024) throw new Exception("Max file size exceeded.");

            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(createPostModel.Image.FileName).ToLower())) throw new Exception("Invalid file type.");

            var uploadFilesPath = Path.Combine(_appHosting.WebRootPath, "Posts/Images");

            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);
            
            var newfilePath = Path.Combine(uploadFilesPath, createPostModel.Image.FileName);

            using (var stream = new FileStream(newfilePath, FileMode.Create))
            {
                await createPostModel.Image.CopyToAsync(stream);
            }

            Post post = new Post
            {
                Title = createPostModel.Title,
                ImageUrl = createPostModel.Image.FileName,
                ShortDescription = createPostModel.ShortDescription
            };
            _context.Posts.Add(post);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

