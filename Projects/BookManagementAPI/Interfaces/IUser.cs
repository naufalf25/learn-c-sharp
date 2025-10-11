namespace BookManagementAPI.Interfaces;

public interface IUser
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}