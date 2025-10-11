using BookManagementAPI.Interfaces;

namespace BookManagementAPI.Models;

public class User : IUser
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
}