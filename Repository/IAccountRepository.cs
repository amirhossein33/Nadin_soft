using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using myproject.Models;
using myproject.Models.Account;

namespace myproject.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignUpDto signUpDto);
        Task<string>Login(LoginDto loginDto);
    }
}