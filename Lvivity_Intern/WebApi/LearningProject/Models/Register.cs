using System.ComponentModel.DataAnnotations;

namespace LearningProject.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "You can enter only letters and numbers")]
        public string UserName { get; set; } 

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\D*\d\D*$", ErrorMessage = "You can enter only letters and one number")]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
