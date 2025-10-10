namespace BookManagementAPI.Interfaces;

public interface IBook
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int PublishedYear { get; set; }
    public IAuthor? Author { get; set; }
}