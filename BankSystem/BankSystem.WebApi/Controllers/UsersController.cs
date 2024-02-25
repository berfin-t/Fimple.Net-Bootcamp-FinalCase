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
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper,UserRepository userRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
    {
        _userRepository.AddUser(userDto);
        return Ok(new { Message = "User registration has been created successfully." });
    }    

    [Authorize(Roles = "Admin")]
    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();
        var userDto = _mapper.Map<IEnumerable<UserDto>>(users);
        return Ok(userDto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("role-assign")]
    public IActionResult AssignUserRole([FromBody] UserModel model, int userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null)
        {
            return NotFound(new { Message = "User not found." });
        }

        _userRepository.AssignUserRole(userId, model.Role);

        return Ok(new { Message = "The user role updated successfully." });
    }

}
