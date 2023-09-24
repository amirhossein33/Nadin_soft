using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using myproject.Models;
using myproject.Models.Account;
using myproject.Repository;

namespace myproject.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUpDto signUpDto)
        {
            var result = await _accountRepository.SignUp(signUpDto);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Unauthorized(result.Errors.Select(x => x.Description).ToList());
        }
          [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result2 = await _accountRepository.Login(loginDto);
            if (string.IsNullOrEmpty(result2))
            {
                return Unauthorized();
            }
            return Ok(result2);
        }
    }
}