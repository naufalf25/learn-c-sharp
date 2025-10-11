using BookManagementAPI.Models;

namespace BookManagementAPI.Interfaces;

public interface IUserRepository
{
    public Task<User?> AuthenticateUserAsync(string email, string password);
    public Task<User> RegisterUserAsync(User user);
    public Task<bool> IsUserExists(string email);
}