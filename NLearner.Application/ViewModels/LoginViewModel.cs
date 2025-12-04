using System.ComponentModel.DataAnnotations;

namespace NLearner.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
