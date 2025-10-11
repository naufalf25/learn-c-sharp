using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BookManagementAPI.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtAuthService _jwtAuthService;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IJwtAuthService jwtAuthService, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtAuthService = jwtAuthService;
        _mapper = mapper;
    }

    public async Task<UserProfileDTO> RegisterUserAsync(RegisterUserDTO userDTO)
    {
        var user = new User
        {
            UserName = userDTO.Email,
            Email = userDTO.Email,
            FullName = userDTO.FullName,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await _userManager.CreateAsync(user, userDTO.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException($"Register user with email {userDTO.Email} failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        Console.WriteLine($"Password hash: {user.PasswordHash}");

        if (!await _roleManager.RoleExistsAsync(userDTO.Role))
            await _roleManager.CreateAsync(new IdentityRole(userDTO.Role));

        await _userManager.AddToRoleAsync(user, userDTO.Role);
        return _mapper.Map<UserProfileDTO>(user);
    }

    public async Task<AuthResponseDTO> LoginUserAsync(LoginUserDTO userDTO)
    {
        var user = await _userManager.FindByEmailAsync(userDTO.Email);
        bool passwordCheck = await _userManager.CheckPasswordAsync(user, userDTO.Password);
        if (user == null || !passwordCheck)
            throw new UnauthorizedAccessException("Invalid Email or Password");

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "user";
        var token = _jwtAuthService.GenerateToken(user, role);
        var response = _mapper.Map<AuthResponseDTO>(user);
        response.Token = token;
        response.Role = role;
        response.ExpiredAt = DateTime.UtcNow.AddMinutes(60);

        return response;
    }
}