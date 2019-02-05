using LearningProject.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace LearningProject
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string ShortDescription { get; set; }

    }
}
