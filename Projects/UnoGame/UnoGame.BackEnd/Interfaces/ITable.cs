namespace UnoGame.BackEnd.Interfaces;

public interface ITable
{
    public ICard TopCard { get; set; }
    public List<ICard> DiscardPile { get; set; }
    public int DiscardCount { get; set; }
}