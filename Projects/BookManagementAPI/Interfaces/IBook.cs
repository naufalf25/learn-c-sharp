namespace BookManagementAPI.Interfaces;

public interface IBook
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; }

    public string UserId { get; set; }
    public IUser? User { get; set; }
}