using System.ComponentModel.DataAnnotations;

namespace NLearner.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} have to be at least {2} and maximum {1} characters long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password doesn't match")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required!")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "The {0} have to be at least {2} and maximum {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
