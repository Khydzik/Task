using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Models
{
    public class Responce<T>
    {
        public T Result { get; set; }
        public Error Error { get; set; }
    }
}
