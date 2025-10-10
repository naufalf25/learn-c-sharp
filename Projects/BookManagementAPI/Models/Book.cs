using System.ComponentModel.DataAnnotations;
using BookManagementAPI.Interfaces;

namespace BookManagementAPI.Models;

public class Book : IBook
{
    public int BookId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public IAuthor? Author { get; set; }
}