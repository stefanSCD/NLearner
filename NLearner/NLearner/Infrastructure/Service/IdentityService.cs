using Microsoft.AspNetCore.Identity;
using NLearner.Domain.Entities;
using NLearner.DTO.Users;

namespace NLearner.Infrastructure.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public Task<User?> FindByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

        public async Task<SignInResult> LoginAsync(string emailOrUserName, string password) => 
            await _signInManager.PasswordSignInAsync(emailOrUserName, password, false, lockoutOnFailure: false);

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task<IdentityResult> RegisterAsync(RegisterAddRequest user, string password)
        {
            var result = await _userManager.CreateAsync(user.ToUser(), password);
            if (result.Succeeded) {
                await _signInManager.SignInAsync(user.ToUser(), isPersistent: false);
            }
            return result;
        }
    }
}
