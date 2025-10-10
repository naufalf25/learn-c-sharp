using System.ComponentModel.DataAnnotations;
using BookManagementAPI.Interfaces;

namespace BookManagementAPI.DTOs;

public class AddBookDTO
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;

    [Required]
    public int PublishedYear { get; set; }

    [Required]
    public IAuthor? Author { get; set; }
}

public class GetBookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public IAuthor? Author { get; set; }
}

public class UpdateBookDTO
{
    [Required]
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public IAuthor? Author { get; set; }
}