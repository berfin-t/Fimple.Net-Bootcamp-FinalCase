using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.WebApi.JwtTokenOperation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly BankingDbContext _context;
    private readonly JwtToken _token;

    public UserController(BankingDbContext context, JwtToken token)
    {
        _context = context;
        _token = token;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserModel userModel)
    {        
        userModel.Password = _token.HashPassword(userModel.Password); 
        _context.Users.Add(userModel);

        _context.SaveChanges();

        return Ok(new { Message = "Kullanıcı kaydı başarıyla oluşturuldu." });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var hashedPassword = _token.HashPassword(model.Password);
        
        var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

        if (user == null)
        {
            return Unauthorized(new { Message = "Geçersiz kullanıcı adı veya şifre." });
        }

        model.Password = _token.HashPassword(model.Password);
        _context.Login.Add(model);
        _context.SaveChanges();

        var token = _token.GenerateJwtToken(user);
        HttpContext.Items["JwtToken"] = token;

        return Ok(new { Token = token });
    }

    [Authorize(Roles = "admin")]
    [HttpGet("get-all-users")]
    public IActionResult GetAllUsers()
    {        

        var users = _context.Users.ToList();
        return Ok(users);

    }

    [Authorize(Roles = "admin")]
    [HttpPut("role-assign")]
    public IActionResult AssignUserRole([FromBody] UserModel model, int userId)
    {
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            return NotFound(new { Message = "Kullanıcı bulunamadı." });
        }

        user.Role = model.Role;
        _context.SaveChanges();

        return Ok(new { Message = "Kullanıcı rolü başarıyla güncellendi." });
    }

}
