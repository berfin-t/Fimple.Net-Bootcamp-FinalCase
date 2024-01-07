using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Application.Repositories;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using BankSystem.WebApi.JwtTokenOperation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BankSystem.Application.Dto;
using System.Collections;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly JwtToken _token;
    private readonly IMapper _mapper;

    public UserController(IMapper mapper,UserRepository userRepository, JwtToken token)
    {
        _userRepository = userRepository;
        _token = token;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] UserDto userDto)
    {
        _userRepository.AddUser(userDto);
        return Ok(new { Message = "User registration has been created successfully." });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var user = _userRepository.GetUserByUsernameAndPassword(model.Username, model.Password);

        if (user == null)
        {
            return Unauthorized(new { Message = "Invalid username or password." });
        }

        _userRepository.AddLogin(model);

        var token = _token.GenerateJwtToken(user);
        HttpContext.Items["JwtToken"] = token;

        return Ok(new { Token = token });
    }

    [Authorize(Roles = "admin")]
    [HttpGet("get-all-users")]
    public IActionResult GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();
        var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(userDto);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("role-assign")]
    public IActionResult AssignUserRole([FromBody] UserModel model, int userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        _userRepository.AssignUserRole(userId, model.Role);

        return Ok(new { Message = "The user role has been updated successfully." });
    }

}
