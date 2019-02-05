using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Models
{
    public class CreatePostModel
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }

        public string ShortDescription { get; set; }
    }
}
