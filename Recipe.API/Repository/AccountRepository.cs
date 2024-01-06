using Microsoft.AspNetCore.Identity;
using Recipe.API.Models;

namespace Recipe.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = signUpModel.FirstName,
                LastName = signUpModel.LastName,
                Username = signUpModel.Username,
                Email = signUpModel.Email,
            };

            return await _userManager.CreateAsync(user, signUpModel.Password);

        }
    }
}
