namespace BookManagementAPI.Interfaces;

public interface IAuthor
{
    public string Name { get; set; }
    public ICollection<IBook> Books { get; set; }
}