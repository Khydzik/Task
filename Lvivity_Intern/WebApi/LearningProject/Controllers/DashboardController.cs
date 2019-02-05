using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using LearningProject.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace LearningProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        public const int DefaultPageRecordCount = 4;
        public const int DefaultPages = 1;
        private ApplicationContext _context;
        private readonly IHostingEnvironment _appHosting;

        public DashboardController(ApplicationContext context, IHostingEnvironment hostingEnvironment)
        {
            _appHosting = hostingEnvironment;
            _context = context;
        }

        [HttpPost]
        public async Task<object> Posts([FromBody]PaginationModel paginationModel)
        {
            var uploadFilesPath = Path.Combine(_appHosting.WebRootPath, "Posts");
            var takePage = paginationModel.CurrentPage == 0 ? DefaultPages : paginationModel.CurrentPage;
            var takeCount = paginationModel.PerPage == 0 ? DefaultPageRecordCount : paginationModel.PerPage;

            var response = new
            {
                post = _context.Posts.Select(posts => new
                {
                    id = posts.Id,
                    title = posts.Title,
                    imageUrl = "Images/" +posts.ImageUrl, 
                    shortDescription = posts.ShortDescription
                }).Skip((takePage - 1) * takeCount)
                .Take(takeCount)
                .ToList()
            };
            return response;
        }
    }
}