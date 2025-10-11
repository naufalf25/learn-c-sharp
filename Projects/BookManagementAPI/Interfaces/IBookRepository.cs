using BookManagementAPI.Models;

namespace BookManagementAPI.Interfaces;

public interface IBookRepository
{
    public Task<IBook> AddBookAsync(Book book);
    public Task<List<IBook>> GetAllBooksAsync();
    public Task<IBook?> GetBookByIdAsync(int bookId);
    public Task<List<IBook>> GetBooksByUserIdAsync(string userId);
    public Task<IBook?> UpdateBookAsync(Book book);
    public Task<bool> DeleteBookAsync(int bookId);
}