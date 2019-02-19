using LearningProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public interface IPostDataStore
    {      
        Task<List<Post>> GetPosts(PaginationModel model);
        Task<bool> IsSavePost(Post post);       
    }
}
