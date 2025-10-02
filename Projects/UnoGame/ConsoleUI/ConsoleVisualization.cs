using System.Drawing;
using System.IO.Compression;
using Enums;
using Interfaces;
using Models;

namespace ConsoleUI;

public static class ConsoleVisualization
{
    private static string _createSeparator(char symbol, int length)
    {
        return new string(symbol, length);
    }

    public static void DrawPlayerInfo(IPlayer player, int currentPlayerNumber)
    {
        Console.WriteLine("Current Player Turn:");
        Console.WriteLine(player.Type == PlayerType.AI
        ? $"Player {currentPlayerNumber} - {player.Name} [AI]"
        : $"Player {currentPlayerNumber} - {player.Name}"
        );
        Console.WriteLine(_createSeparator('=', 30));
    }

    public static void DrawDesk(bool isReversed, int deckCardCount, int discardCount, List<IPlayer> players, IPlayer currentPlayer, Func<IPlayer, List<ICard>> getPlayerHands, ICard topCard, CardColor declaredWildColor)
    {
        Console.WriteLine($"\nDirection: \u001b[31m{(isReversed ? "Counterclockwise" : "Clockwise")}\u001b[0m");
        Console.WriteLine($"Draw pile: {deckCardCount} cards");
        Console.WriteLine($"Discard pile: {discardCount} cards");

        Console.WriteLine(_createSeparator('-', 28));
        foreach (var player in players)
        {
            if (player != currentPlayer)
            {
                Console.WriteLine($"> Player {players.IndexOf(player) + 1} - {player.Name}: {getPlayerHands(player).Count} cards");
            }
        }
        Console.WriteLine(_createSeparator('-', 28));

        Console.WriteLine($"Color: {declaredWildColor}");
        Console.WriteLine($"Top Card: {VisualizeCard(topCard)}");
    }

    public static void DrawPlayerHands(List<ICard> playerHands, IPlayer player, Func<IPlayer, ICard, bool> canPlayCard, ICard topCard)
    {
        Console.WriteLine($"{player.Name} current hands is:");

        var cardsByColor = playerHands
            .OrderBy(card => card.Color)
            .ThenBy(card => card.Number)
            .GroupBy(card => canPlayCard(player, card))
            .ToDictionary(group => group.Key, group => group.ToList());

        foreach (var playableCard in cardsByColor.OrderByDescending(kvp => kvp.Key))
        {
            var block = playableCard.Value.Select(RenderCardBlock).ToList();
            string card = string.Join(" ", block.Select(b => b.card));
            string shortcut = string.Join(" ", block.Select(b => b.shortcut));

            Console.WriteLine(card);
            Console.WriteLine(shortcut);
        }
    }

    public static void DrawChoices(IPlayer currentPlayer, Dictionary<PlayerActions, (string Keybind, string Description)> GetAllChoices)
    {
        Console.WriteLine($"{currentPlayer.Name} please decide:");

        foreach (var choice in GetAllChoices)
        {
            Console.WriteLine($"\u001b[1m[{choice.Value.Keybind.ToUpper()}]\u001b[0m {choice.Value.Description}");
        }
    }

    public static void DrawPlayerCardChoice(List<ICard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {VisualizeCard(cards[i])} {(cards[i].IsWild == true ? cards[i].Action : $"{cards[i].Color} - {(cards[i].Action.HasValue ? cards[i].Action : cards[i].Number)}")}");
        }
    }

    public static void DrawIsStackedActive(IPlayer currentPlayer, int count, ActionType action)
    {
        Console.WriteLine(_createSeparator('=', 28));
        Console.WriteLine($"{currentPlayer.Name}, you are affected by {action} card effect totaling {count} cards.");
        Console.WriteLine("Do you want to counter with another draw card? [y/n]");
    }

    private static (string card, string shortcut) RenderCardBlock(ICard card)
    {
        string colorCode = GetColorCode(card.Color);
        string resetCode = "\u001b[0m";
        string value = GetStringDescription(card, 5);
        string shortcut = CenterText(GetCardShortcut(card), 5);

        string formattedCard = $"{colorCode}{value}{resetCode}";

        return (formattedCard, shortcut);
    }

    private static string VisualizeCard(ICard? card)
    {
        if (card == null) return "     ";

        string valueDescription = GetStringDescription(card, 5);
        string coloredValue = $"{GetColorCode(card.Color ?? null)}{valueDescription}\u001b[0m";

        if (card.Action == ActionType.Skip || card.Action == ActionType.Reverse || card.Action == ActionType.DrawTwo || card.Action == ActionType.Wild || card.Action == ActionType.WildDrawFour) return $"{coloredValue} ";

        return coloredValue;
    }

    private static string GetColorCode(CardColor? cardColor)
    {
        if (cardColor == null) return "\u001b[47;30m";

        return cardColor switch
        {
            CardColor.Red => "\u001b[41;30m",
            CardColor.Green => "\u001b[42;30m",
            CardColor.Yellow => "\u001b[43;30m",
            CardColor.Blue => "\u001b[44;30m",
            CardColor.Wild => "\u001b[47;30m",
            _ => "\u001b[0m"
        };
    }

    private static string GetCardShortcut(ICard card)
    {
        string shortcut = "";
        string colorCode = card.Color != null ? ColorCode((CardColor)card.Color) : "";

        if (card.Number.HasValue) shortcut = $"{colorCode}{(int)card.Number.Value}";

        if (card.Action != null)
        {
            string actionCode = ActionCode((ActionType)card.Action);
            shortcut = $"{colorCode}{actionCode}";
        }

        return shortcut;
    }

    private static string ColorCode(CardColor color)
    {
        return color switch
        {
            CardColor.Red => "R",
            CardColor.Green => "G",
            CardColor.Yellow => "Y",
            CardColor.Blue => "B",
            CardColor.Wild => "W",
            _ => "",
        };
    }

    private static string ActionCode(ActionType action)
    {
        return action switch
        {
            ActionType.Skip => "S",
            ActionType.Reverse => "R",
            ActionType.DrawTwo => "+2",
            ActionType.WildDrawFour => "+4",
            _ => "",
        };
    }

    private static string GetStringDescription(ICard card, int valueLength)
    {
        string valueDescription;

        if (card.Number.HasValue)
        {
            int cardNumber = (int)card.Number;
            valueDescription = cardNumber.ToString();
        }
        else if (card.IsWild == true)
            valueDescription = card.Action == ActionType.WildDrawFour ? "W+4" : "W";
        else if (card.Action != null)
            valueDescription = ActionCode((ActionType)card.Action);
        else
            valueDescription = "";

        return FormattedValueDescription(valueDescription!, valueLength);
    }

    private static string FormattedValueDescription(string valueDescription, int valueLength)
    {
        valueDescription = valueDescription.Trim();

        if (valueDescription.Length == valueLength) return valueDescription;

        int padding = Math.Max(0, (valueLength - valueDescription.Length) / 2);

        string formattedValue = valueDescription.PadLeft(valueDescription.Length + padding).PadRight(valueLength);

        return formattedValue;
    }

    private static string CenterText(string text, int width)
    {
        if (text.Length >= width) return text;
        int padding = (width - text.Length) / 2;
        return text.PadLeft(text.Length + padding).PadRight(width);
    }
}