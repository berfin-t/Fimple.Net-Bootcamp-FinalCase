using BankSystem.Application.Repositories;
using BankSystem.Business.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.WebApi.JwtTokenOperation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtToken _token;
        private readonly LoginRepository _loginRepository;

        public LoginController(JwtToken token, LoginRepository loginRepository)
        {
            _token = token;
            _loginRepository = loginRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _loginRepository.GetUserByUsernameAndPassword(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid username or password." });
            }

            _loginRepository.AddLogin(model);

            var token = _token.GenerateJwtToken(user);
            HttpContext.Items["JwtToken"] = token;

            return Ok(new { Token = token });
        }
        
    }
}
