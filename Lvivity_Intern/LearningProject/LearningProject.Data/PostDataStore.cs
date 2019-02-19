using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using LearningProject.Data.Models;
using System.Threading.Tasks;

namespace LearningProject.Data
{
    public class PostDataStore: IPostDataStore
    {
        private readonly DataBaseContext _context;
        public const string rootFile = "Images/";

        public PostDataStore(DataBaseContext context)
        {
            _context = context;
        }        
        
        public async Task<List<Post>> GetPosts(PaginationModel model)
        {
            var post = await _context.Posts.Select(posts => new Post
            {
                Id = posts.Id,
                Title = posts.Title,
                ImageUrl = rootFile + posts.ImageUrl,
                ShortDescription = posts.ShortDescription
            }).Skip((model.CurrentPage - 1) * model.PerPage)
              .Take(model.PerPage)
              .ToListAsync();

            return post;
        }
        
        public async Task<bool> IsSavePost(Post post)
        {
            await _context.Posts.AddAsync(post);

            await _context.SaveChangesAsync();

            return true;
        }        
    }
}
