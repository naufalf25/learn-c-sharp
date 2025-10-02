using UnoGame.BackEnd.Enums;
using UnoGame.BackEnd.Interfaces;
using UnoGame.BackEnd.Models;

namespace UnoGame.BackEnd.Controller;

public class DeckBuilder
{
    public static IDeck BuildDeck()
    {
        Deck deck = new();

        foreach (CardColor color in Enum.GetValues<CardColor>())
        {
            if (color == CardColor.Wild) break;

            deck.Cards.Add(new Card(color, CardNumber.Zero, null));

            for (int i = 1; i <= 9; i++)
            {
                var cardNumber = (CardNumber)i;
                deck.Cards.Add(new Card(color, cardNumber, null));
                deck.Cards.Add(new Card(color, cardNumber, null));
            }

            for (int i = 0; i < 2; i++)
            {
                deck.Cards.Add(new Card(color, null, ActionType.Skip));
                deck.Cards.Add(new Card(color, null, ActionType.Reverse));
                deck.Cards.Add(new Card(color, null, ActionType.DrawTwo));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            deck.Cards.Add(new Card(CardColor.Wild, null, ActionType.Wild));
            deck.Cards.Add(new Card(CardColor.Wild, null, ActionType.WildDrawFour));
        }

        return deck;
    }
}