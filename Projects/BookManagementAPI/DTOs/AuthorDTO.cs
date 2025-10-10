using System.ComponentModel.DataAnnotations;
using BookManagementAPI.Interfaces;

namespace BookManagementAPI.DTOs;

public class GetAuthorDTO
{
    public string Name { get; set; } = string.Empty;
    public ICollection<IBook> Books { get; set; } = [];
}