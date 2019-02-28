using LearningProject.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public interface IPostService
    {
        Task<Post> CreatePost(string title, string shortDescription, string imageName, byte[] imageData);
        Task<List<Post>> GetPosts(int take, int skip);
    }
}
