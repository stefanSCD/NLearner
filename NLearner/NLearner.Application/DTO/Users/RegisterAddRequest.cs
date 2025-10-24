using NLearner.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace NLearner.DTO.Users
{
    public class RegisterAddRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public User ToUser()
        {
            return new User()
            {
                Email = Email,
                UserName = UserName,
            };
        }
    }
}
