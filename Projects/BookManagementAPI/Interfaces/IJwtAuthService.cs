using BookManagementAPI.Models;

namespace BookManagementAPI.Interfaces;

public interface IJwtAuthService
{
    public string GenerateToken(User user);
}