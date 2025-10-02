using UnoGame.BackEnd.Enums;

namespace UnoGame.BackEnd.Interfaces;

public interface ICard
{
    public CardColor? Color { get; }
    public CardNumber? Number { get; }
    public ActionType? Action { get; }
    public bool IsWild { get; set; }
    public string DisplayName { get; set; }
}