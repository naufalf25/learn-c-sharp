using Enums;
using Interfaces;

namespace ConsoleUI;

public static class ConsoleVirtualization
{
    private static string _createSeparator(char symbol, int length)
    {
        return new string(symbol, length);
    }

    public static void DrawPlayerInfo(IPlayer player)
    {
        Console.WriteLine("Current Player Turn:");
        Console.WriteLine(player.Type == PlayerType.AI
        ? $"{player.Name} [AI]"
        : player.Name
        );
        Console.WriteLine(_createSeparator('=', 30));
    }

    public static void DrawCurrentDirection(bool isReversed)
    {
        Console.WriteLine($"Direction: \u001b[31m{(isReversed ? "Counterclockwise" : "Clockwise")}\u001b[0m");
    }

    public static void DrawCurrentCard(int deckCardCount, int discardCount)
    {
        Console.WriteLine($"Draw pile: {deckCardCount} cards");
        Console.WriteLine($"Discard pile: {discardCount} cards");
    }

    public static void DrawOtherPlayerCards(List<IPlayer> players, IPlayer currentPlayer, Func<IPlayer, List<ICard>> getPlayerHands)
    {
        Console.WriteLine(_createSeparator('-', 28));
        foreach (var player in players)
        {
            if (player != currentPlayer)
            {
                Console.WriteLine($"> {player.Name}: {getPlayerHands(player).Count} cards");
            }
        }
        Console.WriteLine(_createSeparator('-', 28));
    }
}