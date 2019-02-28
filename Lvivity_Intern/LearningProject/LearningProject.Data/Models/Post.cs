

using System.ComponentModel.DataAnnotations;

namespace LearningProject.Data.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string ShortDescription { get; set; }
    }
}
