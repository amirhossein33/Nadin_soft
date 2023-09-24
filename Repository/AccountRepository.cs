using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using myproject.Data;
using myproject.Models;


using Microsoft.Extensions.Configuration;
using myproject.Models.Account;

namespace myproject.Repository
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
        public async Task<IdentityResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
            {
                Firstname = signUpDto.Firstname,
                Lastname = signUpDto.Lastname,
                Email = signUpDto.EmailAddress,
                UserName = signUpDto.EmailAddress
            };
            return await _userManager.CreateAsync(user, signUpDto.Password);
        }
                public async Task<string> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!result.Succeeded)
            {
                return null;
            }
          
            return NewMethod(loginDto);
        }

        private string NewMethod(LoginDto loginDto)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , loginDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudiance"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)


            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}