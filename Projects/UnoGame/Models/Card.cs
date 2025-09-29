namespace Models;

using Enums;
using Interfaces;

public class Card : ICard
{
    public CardColor? Color { get; }
    public CardNumber? Number { get; }
    public ActionType? Action { get; }
    public bool IsWild { get; set; }
    public string DisplayName { get; set; }

    public Card(CardColor? color, CardNumber? number, ActionType? action)
    {
        Color = color;
        Number = number;
        Action = action;
        IsWild = action == ActionType.Wild || action == ActionType.WildDrawFour;
        DisplayName = color.HasValue ? $"{color} {(action.HasValue ? action : number)}" : $"{action}";
    }
}