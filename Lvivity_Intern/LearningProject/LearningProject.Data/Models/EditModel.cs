using System.ComponentModel.DataAnnotations;

namespace LearningProject.Data.Models
{
    public class EditModel
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public int roleId { get; set; }
    }
}
