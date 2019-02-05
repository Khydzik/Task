using LearningProject.Models;

namespace LearningProject
{
    public class JsonOutputWrapper<T>
    {
        public object Result { get; set; }

        public Error Error { get; set; }
    }
}