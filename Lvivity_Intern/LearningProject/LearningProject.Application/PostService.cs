using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using LearningProject.Data.Models;
using System.IO;
using LearningProject.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LearningProject.Application
{
    public class PostService : IPostService
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IHostingEnvironment _appHosting;
        private readonly IRepository<Post> _postRepository;

        public const string rootFile = "Posts/Images";       

        public PostService(IRepository<Post> postRepository, IHostingEnvironment app)
        {
            _postRepository = postRepository;
            _appHosting = app;
        }

        public async Task<List<Post>> GetPosts(int take, int skip)
        {

            return await _postRepository.Query().Skip(skip - 1).Take(take).ToListAsync();       
        }

        public async Task<Post> CreatePost(string title, string shortDescription, string imageName, byte[] imageData)
        {          
            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(imageName).ToLower())) throw new Exception("Invalid file type.");

            var newpost = new Post
            {
                Title = title,
                ShortDescription = shortDescription,
                ImageUrl = imageName
            };

            var post = await _postRepository.InsertAsync(newpost);
            
            var uploadFilesPath = Path.Combine(_appHosting.WebRootPath, rootFile);

            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);

            var newfilePath = Path.Combine(uploadFilesPath, imageName);

            using (var stream = new FileStream(newfilePath, FileMode.Create))
            {
                await stream.WriteAsync(imageData, 0, imageData.Length);
            }

            return post;
        }       
    }
}
