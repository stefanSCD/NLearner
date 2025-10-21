using Microsoft.AspNetCore.Identity;
using NLearner.Domain.Entities;
using NLearner.DTO.Users;

namespace NLearner.Infrastructure.Service
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(RegisterAddRequest user, string password);
        Task<SignInResult> LoginAsync(string emailOrUserName, string password);
        Task LogoutAsync();
        Task<User?> FindByEmailAsync(string email);
    }
}
