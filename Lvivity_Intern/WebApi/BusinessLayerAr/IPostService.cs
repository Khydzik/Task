using LearningProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LearningProject.Application
{
    public interface IPostService
    {     
       Task<bool> IsCreateNewPost(CreatePostModel createPostModel);
       Task<List<Post>> GetPosts(PaginationModel paginationModel);
    }
}
