using System.ComponentModel.DataAnnotations;
using BookManagementAPI.Interfaces;

namespace BookManagementAPI.Models;

public class Author : IAuthor
{
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    public ICollection<IBook> Books { get; set; } = [];
}