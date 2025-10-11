using BookManagementAPI.Data;
using BookManagementAPI.Interfaces;
using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IBook> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<List<IBook>> GetAllBooksAsync()
    {
        List<IBook> books = [.. (await _context.Books.ToListAsync()).Cast<IBook>()];
        return books;
    }

    public async Task<IBook?> GetBookByIdAsync(int bookId)
    {
        return await _context.Books.FindAsync(bookId);
    }

    public async Task<List<IBook>> GetBooksByUserIdAsync(string usedId)
    {
        List<IBook> books = [.. (await _context.Books.Where(b => b.UserId == usedId).ToListAsync()).Cast<IBook>()];
        return books;
    }

    public async Task<IBook?> UpdateBookAsync(Book book)
    {
        var existingBook = await _context.Books.FindAsync(book.BookId);
        if (existingBook == null) return null;

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Genre = book.Genre;
        existingBook.PublishedDate = book.PublishedDate;
        existingBook.Description = book.Description;

        await _context.SaveChangesAsync();
        return existingBook;
    }

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        var targetBook = await _context.Books.FindAsync(bookId);
        if (targetBook == null) return false;

        _context.Books.Remove(targetBook);
        await _context.SaveChangesAsync();
        return true;
    }
}