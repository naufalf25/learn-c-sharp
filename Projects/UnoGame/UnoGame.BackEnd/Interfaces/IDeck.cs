namespace UnoGame.BackEnd.Interfaces;

public interface IDeck
{
    public List<ICard> Cards { get; set; }
    public bool IsEmpty { get; set; }
    public int Count { get; set; }
}