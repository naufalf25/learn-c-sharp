namespace BookManagementAPI.Interfaces;

public interface IUser
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
}