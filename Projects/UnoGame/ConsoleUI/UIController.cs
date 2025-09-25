using Controller;
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
            ConsoleVirtualization.DrawPlayerInfo(currentPlayer);

            ConsoleVirtualization.DrawCurrentDirection(_gameController.IsReversed());
            ConsoleVirtualization.DrawCurrentCard(_gameController.GetRemainingCardsInDeck(), _gameController.GetCardsOnTable());

            ConsoleVirtualization.DrawOtherPlayerCards(_gameController.GetAllPlayers(), currentPlayer, _gameController.GetPlayerHand);

            break;
        }
    }
}