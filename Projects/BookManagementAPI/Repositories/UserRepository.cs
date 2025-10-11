using BookManagementAPI.Data;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories;

public class UserRepository : IUserRepository
{
    public readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IUser?> AuthenticateUserAsync(string email, string password)
    {
        var hasher = new PasswordHasher<IUser>();
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hasher.HashPassword(u, password));
    }

    public async Task<IUser> RegisterUserAsync(User user)
    {
        user.CreatedAt = DateTime.UtcNow;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> IsUserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
}