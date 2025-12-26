using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GrandPneu.Api.Services;
using GrandPneu.Api.DTOs;
using GrandPneu.Domain.Entities;
using Microsoft.Extensions.Configuration;
using GrandPneu.Api.Helpers;


namespace GrandPneu.Api.Controllers;


[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;

    public UsersController(UserService userService, IConfiguration config)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest(new { message = "Campos obrigatórios faltando" });

        try
        {
            var user = await _userService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), user);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _userService.LoginAsync(dto);
        if (user == null)
            return Unauthorized(new { message = "Email ou senha inválidos" });

        var token = JwtHelper.GenerateToken(user, _config);
        return Ok(new { user, token });
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDto dto)
    {
        try
        {
            var actorEmail = User.Identity?.Name;
            if (actorEmail == null) return Unauthorized();

            var actor = await _userService.GetByEmailAsync(actorEmail);

            var updatedUser = await _userService.UpdateAsync(id, dto, actor);
            return Ok(updatedUser);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



}
