namespace BookManagementAPI.Interfaces;

public interface IUser
{
    public string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
}