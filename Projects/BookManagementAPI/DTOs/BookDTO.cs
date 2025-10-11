namespace BookManagementAPI.DTOs;

public class RegisterBookDTO
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class BookProfileDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
}

public class UpdateBookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
}