using BookManagementAPI.DTOs;

namespace BookManagementAPI.Interfaces;

public interface IUserService
{
    public Task<UserProfileDTO> RegisterUserAsync(RegisterUserDTO userDTO);
    public Task<AuthResponseDTO> LoginUserAsync(LoginUserDTO userDTO);
}