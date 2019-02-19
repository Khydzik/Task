using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using LearningProject.Data.Models;
using System.IO;
using LearningProject.Data;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public class PostService : IPostService
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IHostingEnvironment _appHosting;
        private readonly IPostDataStore _postDataStore;

        public PostService(IPostDataStore dataStore, IHostingEnvironment app)
        {
            _postDataStore = dataStore;
            _appHosting = app;
        }
        public PostService(IPostDataStore dataStore)
        {
            _postDataStore = dataStore;
        }        

        public async Task<List<Post>> GetPosts(PaginationModel paginationModel)
        {
            var post = await _postDataStore.GetPosts(paginationModel);
            return post;
        }      

        public async Task<bool> IsCreateNewPost(CreatePostModel createPostModel)
        {
            if(createPostModel == null) throw new NullReferenceException("Null File");

            if (createPostModel.Image == null) throw new NullReferenceException("Empty File");           

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
           
            var savePost = await _postDataStore.IsSavePost(post);

            return savePost;
        }       
    }
}
