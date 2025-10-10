using BookManagementAPI.Interfaces;

namespace BookManagementAPI.Models;

public class User : IUser
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
}