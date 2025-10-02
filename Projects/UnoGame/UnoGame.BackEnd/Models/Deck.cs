using UnoGame.BackEnd.Interfaces;

namespace UnoGame.BackEnd.Models;

public class Deck : IDeck
{
    public List<ICard> Cards { get; set; }
    public bool IsEmpty { get; set; }
    public int Count { get; set; }

    public Deck()
    {
        Cards = [];
        IsEmpty = true;
        Count = 0;
    }
}