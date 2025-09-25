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
}