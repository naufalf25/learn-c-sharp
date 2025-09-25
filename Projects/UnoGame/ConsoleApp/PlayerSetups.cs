using Controller;
using Enums;
using Interfaces;
using Models;

namespace ConsoleApp;

public class PlayerSetups
{
    public static List<IPlayer> ConfigurePlayers(IDeck deck, ITable table)
    {
        List<IPlayer> players = [];

        int playerCount;
        while (true)
        {
            Console.Write("How many player you want to set (2 - 4): ");
            string userChoice = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(userChoice)) userChoice = "3";

            if (int.TryParse(userChoice, out playerCount))
            {
                if (playerCount >= 2 && playerCount <= 4) break;
                Console.WriteLine("Number not in range...");
            }
        }

        for (int i = 1; i <= playerCount; i++)
        {
            string? inputType = "";
            while (true)
            {
                Console.Write($"Set Player Type of Player-{i} (H - Human | A - AI/Computer): ");
                inputType = Console.ReadLine()?.ToUpper().Trim();

                if (inputType == "H" || inputType == "A") break;
                Console.WriteLine("Invalid player type! Please enter 'H' for Human or 'A' for AI/Computer");
            }

            PlayerType playerType = inputType switch
            {
                "H" => PlayerType.Human,
                "A" => PlayerType.AI,
                _ => throw new InvalidOperationException("Unexpected index player type")
            };

            string? playerName = "";
            while (true)
            {
                Console.Write($"Enter the Name {playerType} of Player-{i}: ");
                string userInput = Console.ReadLine()?.Trim();
                playerName = $"Player {i} - {userInput}";

                if (!string.IsNullOrWhiteSpace(playerName) && playerName.Length > 0) break;
                Console.WriteLine("Parse error...");
            }

            players.Add(new Player(playerName, playerType));
        }

        return players;
    }
}