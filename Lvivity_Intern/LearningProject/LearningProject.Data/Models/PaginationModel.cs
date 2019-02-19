using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningProject.Data.Models
{
    public class PaginationModel
    {
        [Range(1, 100, ErrorMessage = "There are no such pages")]
        public int Take { get; set; }
        [Range(1, 100, ErrorMessage = "There are no such articles")]
        public int Skip { get; set; }
    }
}
