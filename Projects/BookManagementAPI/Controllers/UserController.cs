using BookManagementAPI.DTOs;
using BookManagementAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
    {
        try
        {
            var profile = await _userService.RegisterUserAsync(userDTO);
            return Ok(new
            {
                status = "success",
                data = profile
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user registration for {Email}", userDTO.Email);
            return StatusCode(500, new { message = "Registration failed due to server error" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
    {
        try
        {
            var response = await _userService.LoginUserAsync(userDTO);
            return Ok(new
            {
                message = "success",
                data = response
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during user login for {Email}", userDTO.Email);
            return StatusCode(500, new { message = "Login failed due to server error" });
        }
    }
}