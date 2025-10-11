using BookManagementAPI.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BookManagementAPI.Models;

public class User : IdentityUser, IUser
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}