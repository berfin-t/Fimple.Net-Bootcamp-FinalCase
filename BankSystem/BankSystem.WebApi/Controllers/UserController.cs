using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.WebApi.JwtTokenOperation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

//[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        //var loginModel = new LoginModel();
        //loginModel.Username = userModel.Username;
        //loginModel.Password = userModel.Password;
        //loginModel.UserId = userModel.UserId;
        //_context.LoginModels.Add(loginModel);

        _context.SaveChanges();

        return Ok(new { Message = "Kullanıcı kaydı başarıyla oluşturuldu." });
    }

    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var hashedPassword = _token.HashPassword(model.Password);
        
        var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == hashedPassword);

        if (user == null)
        {
            return Unauthorized(new { Message = "Geçersiz kullanıcı adı veya şifre." });
        }

        var token = _token.GenerateJwtToken(user);
        HttpContext.Items["JwtToken"] = token;

        return Ok(new { Token = token });
    }

    [Authorize(Roles = "admin")]
    [HttpGet("get-all-users")]
    public IActionResult GetAllUsers()
    {
        var user = User.FindFirst(ClaimTypes.Role)?.Value;

        //var storedToken = HttpContext.Items["JwtToken"] as string;
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // JWT'yi doğrula ve içeriğini al
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        // Kullanıcının rollerini kontrol et
        var roles = jsonToken?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


        var expirationTime = jsonToken?.ValidTo;

        // Eğer kullanıcının "admin" rolü yoksa erişimi reddet
        if (string.IsNullOrEmpty(roles) || !roles.Split(',').Contains("admin"))
        {
            return Forbid();
        }

        var users = _context.Users.ToList();
        return Ok(users);

    }

    //[Authorize(Roles = "admin")]
    //[HttpPost("assign-role")]
    //public IActionResult AssignUserRole([FromBody] UserRoleModel model)
    //{
    //    var user = _context.Users.Find(model.UserId);

    //    if (user == null)
    //    {
    //        return NotFound(new { Message = "Kullanıcı bulunamadı." });
    //    }

    //    user.Role = model.RoleName;
    //    _context.SaveChanges();

    //    return Ok(new { Message = "Kullanıcı rolü başarıyla güncellendi." });
    //}

}
