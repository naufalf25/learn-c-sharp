namespace BookManagementAPI.Interfaces;

public interface IUser
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; }
}