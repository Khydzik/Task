
namespace LearningProject.Data.Models
{
    public class ResponseLogin<T>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Access_token { get; set; }
        public T Role { get; set; }
    }
}

