using BookManagementAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookManagementAPI.Models;

public class User : IdentityUser, IUser
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = "user";
    public DateTime CreatedAt { get; set; }
}