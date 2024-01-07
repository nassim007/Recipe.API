using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Recipe.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Recipe.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
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

        public async Task<string> LoginAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);
            if (!result.Succeeded) {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
               signingCredentials: new SigningCredentials(authSigninkey, SecurityAlgorithms.HmacSha256Signature)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
