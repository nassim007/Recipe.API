using Microsoft.AspNetCore.Identity;
using Recipe.API.Models;

namespace Recipe.API.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel signUpModel);
    }
}
