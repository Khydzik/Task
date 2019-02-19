using LearningProject.Web.Models;

namespace LearningProject.Web.Models
{
    public class JsonOutputWrapper<T>
    {
        public T Result { get; set; }

        public Error Error { get; set; }
    }
}