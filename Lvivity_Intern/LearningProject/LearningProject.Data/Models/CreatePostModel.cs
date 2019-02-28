using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LearningProject.Data.Models
{
    public class CreatePostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string ShortDescription { get; set; }
    }
}
