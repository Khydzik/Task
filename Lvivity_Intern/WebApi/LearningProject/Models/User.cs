using System.ComponentModel.DataAnnotations;

namespace LearningProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "You can enter only letters and numbers")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\D*\d\D*$", ErrorMessage = "You can enter only letters and one number")]
        public string Password { get; set; }
    }
}
