using BookManagementAPI.Interfaces;

namespace BookManagementAPI.Models;

public class Book : IBook
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public IUser? User { get; set; }
}