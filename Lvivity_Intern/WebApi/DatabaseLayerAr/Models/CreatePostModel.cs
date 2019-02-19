using Microsoft.AspNetCore.Http;

namespace LearningProject.Data.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }

        public IFormFile Image { get; set; }

        public string ShortDescription { get; set; }
    }
}
