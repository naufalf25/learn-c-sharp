using BookManagementAPI.Models;

namespace BookManagementAPI.Interfaces;

public interface IUserRepository
{
    public Task<IUser?> AuthenticateUserAsync(string email, string password);
    public Task<IUser> RegisterUserAsync(User user);
    public Task<bool> IsUserExists(string email);
}