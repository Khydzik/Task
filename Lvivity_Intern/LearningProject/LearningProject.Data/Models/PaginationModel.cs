using System.ComponentModel.DataAnnotations;

namespace LearningProject.Data.Models
{
    public class PaginationModel
    {
        [Required]
        [Range(1, 100, ErrorMessage = "There are no such pages")]
        public int Take { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "There are no such articles")]
        public int Skip { get; set; }
    }
}
