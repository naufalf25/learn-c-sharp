using UnoGame.BackEnd.Controller;
using UnoGame.BackEnd.Enums;
using UnoGame.BackEnd.Interfaces;

namespace UnoGame.ConsoleUI;

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
            Console.Clear();
            IPlayer currentPlayer = _gameController.GetCurrentPlayer();
            int currentPlayerNumber = _gameController.GetAllPlayers().IndexOf(currentPlayer) + 1;

            if (currentPlayer.Type == PlayerType.AI)
            {
                Console.WriteLine($"\n{currentPlayer.Name} turn...");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine($"\n{currentPlayer.Name} turn.\nMake sure you are alone looking at the screen! Press anything to continue...");
                Console.ReadLine();
            }

            ConsoleVisualization.DrawPlayerInfo(currentPlayer, currentPlayerNumber);
            DrawGameInfo();
        }
    }

    private void DrawGameInfo()
    {
        IPlayer currentPlayer = _gameController.GetCurrentPlayer();

        while (true)
        {
            if (currentPlayer.Type == PlayerType.AI) break;

            ConsoleVisualization.DrawDesk(_gameController.IsReversed(), _gameController.GetRemainingCardsInDeck(), _gameController.GetCardsOnTable(), _gameController.GetAllPlayers(), currentPlayer, _gameController.GetPlayerHand, _gameController.GetTopCardFromTable(), _gameController.DeclaredCardColor());

            if (_gameController.IsStackedActive)
            {
                ConsoleVisualization.DrawIsStackedActive(currentPlayer, _gameController.StackedDrawCount, (ActionType)_gameController.StackedCardAction);
                string? playerInput = Console.ReadLine()?.Trim().ToLower();
                HandleStackableDraw(currentPlayer, playerInput);
                break;
            }

            ConsoleVisualization.DrawPlayerHands(_gameController.GetPlayerHand(currentPlayer), currentPlayer, _gameController.CanPlayCard, _gameController.GetTopCardFromTable());
            ConsoleVisualization.DrawChoices(currentPlayer, GetAllChoices);
            Console.WriteLine("Your choice:");
            string playerChoice = Console.ReadLine()!.ToLower().Trim();
            if (!string.IsNullOrWhiteSpace(playerChoice))
            {
                var action = ProcessPlayerChoice(playerChoice);
                if (action != null)
                {
                    if (action == PlayerActions.Play && ProcessPlayerAction((PlayerActions)action))
                        break;
                    else if (action == PlayerActions.Draw)
                    {
                        _gameController.DrawCard(currentPlayer);
                        break;
                    }
                    else if (action == PlayerActions.CallUno)
                    {
                        _gameController.CallUno(currentPlayer);
                        break;
                    }
                    else if (action == PlayerActions.EndGame)
                    {
                        _gameController.EndGame();
                        break;
                    }
                    break;
                }
            }
        }

    }

    private void HandleStackableDraw(IPlayer player, string playerInput)
    {
        if (playerInput == "y")
        {
            ICard matchingCard = _gameController.MatchingStackedCard(player);
            _gameController.HandleStackableDraw(matchingCard, player, true);
            if (matchingCard != null && matchingCard.IsWild)
            {
                ProcessPlayCard(player, matchingCard, true);
            }
        }
        else if (playerInput == "n")
        {
            _gameController.HandleStackableDraw(null, player, false);
        }
        else
        {
            Console.WriteLine("Wrong shortcut! Please type 'y' for YES or 'n' for NO");
        }
    }

    private bool ProcessPlayerAction(PlayerActions action)
    {
        IPlayer currentPlayer = _gameController.GetCurrentPlayer();
        List<ICard> playerHands = _gameController.GetPlayerHand(currentPlayer);

        if (action == PlayerActions.Play)
        {
            ConsoleVisualization.DrawPlayerCardChoice(playerHands);

            Console.WriteLine("Your choice:");
            if (int.TryParse(Console.ReadLine().Trim() ?? "0", out int cardChoice))
            {
                if (cardChoice >= 1 && cardChoice <= playerHands.Count)
                {
                    var selectedCard = playerHands[cardChoice - 1];
                    if (ProcessPlayCard(currentPlayer, selectedCard, false)) return true;
                }
                else
                {
                    Console.WriteLine("\u001b[31mInput number is out of range!\u001b[0m");
                    Console.WriteLine();
                }
            }
        }

        return false;
    }

    private bool ProcessPlayCard(IPlayer player, ICard card, bool isStacked)
    {
        if (card.IsWild == true)
        {
            Console.WriteLine("Choose new color (red, yellow, green blue):");
            string? playerChoose = Console.ReadLine();

            if (Enum.TryParse(playerChoose, true, out CardColor chosenColor))
                _gameController.DeclareWildColor(chosenColor);
            else
                _gameController.DeclareWildColor(CardColor.Red);
        }

        if (isStacked) return true;
        if (_gameController.PlayCard(player, card)) return true;

        return false;
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