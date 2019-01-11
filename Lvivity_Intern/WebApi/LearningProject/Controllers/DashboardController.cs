using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        [HttpGet]
        public List<Post> GetPosts()
        {
            List<Post> posts = new List<Post>
            {
                new Post{Title = "News1" , ImageUrl =
                "https://images.unian.net/photos/2018_11/1542365481-1591.jpg?0.35475568642849686", ShortDescription = "Why do people choose a " +
                "dog that looks like them? The answer to this question - more scientifically " +
                "substantiated than one could expect - reveals something about the close relationships that we, people, create with their " +
                "four-legged friends. Moreover, the research shows some rather unexpected parallels and with other relationships in " +
                "our lives - the choice of a romantic partner."},
                new Post{Title = "News2", ImageUrl = "https://images.unian.net/photos/2018_11/1542365481-1591.jpg?0.35475568642849686",ShortDescription = "The Eiffel Tower is beautiful at any time of day and time of year. On a sunny morning, she looks" +
                    " young, thin and graceful, and with the onset of twilight, she is highlighted and looks even more impressive. Every day " +
                    "at 19:00, the Eiffel Tower turns on the illumination and thousands of flashing lights shake the mind of astonished " +
                    "travelers."},
                new Post{Title = "News3", ImageUrl = "https://images.unian.net/photos/2018_11/1542365481-1591.jpg?0.35475568642849686",
                    ShortDescription = "Birds are an integral part of the natural environment, they are widespread wherever there " +
                    "are favorable conditions on earth for their lives. "}
            };
            return posts;
        }
    }
}