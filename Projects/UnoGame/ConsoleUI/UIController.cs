using Controller;
using Enums;
using Interfaces;

namespace ConsoleUI;

public class UIController
{
    private readonly UnoGameController _gameController;

    public UIController(UnoGameController gameController)
    {
        _gameController = gameController;
    }

    public void Run()
    {
        Console.Clear();

        while (_gameController.GetGameStatus() == "In Progress")
        {
            IPlayer currentPlayer = _gameController.GetCurrentPlayer();
            int currentPlayerNumber = _gameController.GetAllPlayers().IndexOf(currentPlayer) + 1;

            if (currentPlayer.Type == PlayerType.AI)
            {
                Console.WriteLine($"{currentPlayer.Name} turn...");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"{currentPlayer.Name} turn.\nMake sure you are alone looking at the screen! Press anything to continue...");
                Console.ReadLine();
            }

            ConsoleVisualization.DrawPlayerInfo(currentPlayer, currentPlayerNumber);
            DrawGameInfo();

            break;
        }
    }

    private void DrawGameInfo()
    {
        IPlayer currentPlayer = _gameController.GetCurrentPlayer();

        while (true)
        {
            ConsoleVisualization.DrawDesk(_gameController.IsReversed(), _gameController.GetRemainingCardsInDeck(), _gameController.GetCardsOnTable(), _gameController.GetAllPlayers(), currentPlayer, _gameController.GetPlayerHand, _gameController.GetTopCardFromTable());

            ConsoleVisualization.DrawPlayerHands(_gameController.GetPlayerHand(currentPlayer), currentPlayer, _gameController.CanPlayCard, _gameController.GetTopCardFromTable());

            if (currentPlayer.Type == PlayerType.AI) break;

            ConsoleVisualization.DrawChoices(currentPlayer, GetAllChoices);

            Console.WriteLine("Your choice:");
            string playerChoice = Console.ReadLine()!.ToLower().Trim();
            if (!string.IsNullOrWhiteSpace(playerChoice))
            {
                Console.WriteLine($"{currentPlayer.Name} choose {playerChoice}");
                var action = ProcessPlayerChoice(playerChoice);

                if (action != null)
                {
                    ProcessPlayerAction((PlayerActions)action);
                    break;
                }
            }
        }

    }

    private void ProcessPlayerAction(PlayerActions action)
    {
        IPlayer currentPlayer = _gameController.GetCurrentPlayer();
        List<ICard> playerHands = _gameController.GetPlayerHand(currentPlayer);

        while (true)
        {
            if (action == PlayerActions.Play)
            {
                ConsoleVisualization.DrawPlayerCardChoice(playerHands);

                Console.WriteLine("Your choice:");
                int cardChoice = int.Parse(Console.ReadLine().Trim() ?? "0");
                if (cardChoice >= 1 && cardChoice < playerHands.Count)
                {
                    var selectedCard = playerHands[cardChoice - 1];
                    Console.WriteLine($"Player select card: {selectedCard.Color} - {selectedCard.Number}");
                }
            }
        }
    }

    private static void ProcessPlayCard(ICard card)
    {

    }

    private static readonly Dictionary<PlayerActions, (string Keybind, string Description)> GetAllChoices = new()
    {
        { PlayerActions.Play, ("p", "Play a card") },
        { PlayerActions.Draw, ("d", "Draw a card") },
        { PlayerActions.CallUno, ("c", "Call UNO!") },
        { PlayerActions.EndGame, ("e", "End the Game") }
    };

    private static PlayerActions? ProcessPlayerChoice(string playerChoice)
    {
        var playerAction = GetAllChoices.FirstOrDefault(kvp => kvp.Value.Keybind.Equals(playerChoice.ToLower()));
        return playerAction.Key;
    }
}