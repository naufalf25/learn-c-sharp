using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;

namespace BookManagementAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtAuthService _jwtAuthService;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IJwtAuthService jwtAuthService, IMapper mapper)
    {
        _userRepository = userRepository;
        _jwtAuthService = jwtAuthService;
        _mapper = mapper;
    }

    public async Task<UserProfileDTO> RegisterUserAsync(RegisterUserDTO userDTO)
    {
        if (await _userRepository.IsUserExists(userDTO.Email))
            throw new InvalidOperationException($"User with email {userDTO.Email} already registered");

        var user = _mapper.Map<User>(userDTO);
        var createdUser = await _userRepository.RegisterUserAsync(user);
        return _mapper.Map<UserProfileDTO>(createdUser);
    }

    public async Task<AuthResponseDTO> LoginUserAsync(LoginUserDTO userDTO)
    {
        var user = await _userRepository.AuthenticateUserAsync(userDTO.Email, userDTO.Password)
            ?? throw new UnauthorizedAccessException("Invalid credentials");

        var token = _jwtAuthService.GenerateToken(user);
        var response = _mapper.Map<AuthResponseDTO>(user);
        response.Token = token;

        return response;
    }
}