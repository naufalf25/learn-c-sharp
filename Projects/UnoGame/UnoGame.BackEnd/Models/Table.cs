using UnoGame.BackEnd.Interfaces;

namespace UnoGame.BackEnd.Models;

public class Table : ITable
{
    public ICard TopCard { get; set; }
    public List<ICard> DiscardPile { get; set; }
    public int DiscardCount { get; set; }

    public Table()
    {
        TopCard = new Card(null, null, null);
        DiscardPile = [];
        DiscardCount = 0;
    }
}