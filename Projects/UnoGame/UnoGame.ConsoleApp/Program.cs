using UnoGame.ConsoleApp;
using UnoGame.ConsoleUI;
using UnoGame.BackEnd.Controller;
using UnoGame.BackEnd.Interfaces;
using UnoGame.BackEnd.Models;

IDeck deck = new Deck();
ITable table = new Table();

var mainMenu = ProgramMenus.GetMainMenu(StartGame, ExitGame);

// ===============MAIN===============
mainMenu.Run();

// ===============END================
return;

string? StartGame()
{
    List<IPlayer> players = PlayerSetups.ConfigurePlayers(deck, table);

    UnoGameController unoGame = new(players, deck, table);
    unoGame.OnGameAction += HandleGameAction;
    unoGame.OnCardPlayed += HandleCardPlayed;
    unoGame.OnPlayerWin += HandleWinner;
    unoGame.StartGame();

    var UIController = new UIController(unoGame);
    UIController.Run();

    Console.WriteLine($"GameStatus: {unoGame.GetGameStatus()}");

    return null;
}

string? ExitGame()
{
    return null;
}

void HandleGameAction(string payload)
{
    Console.WriteLine(payload);
    Thread.Sleep(1000);
}

void HandleCardPlayed(IPlayer player, ICard card)
{
    Console.WriteLine($"{player.Name} played card {(card.IsWild ? card.Action : $"{card.Color} - {(card.Action.HasValue ? card.Action : card.Number)}")}");
    Thread.Sleep(500);
}

void HandleWinner(IPlayer player)
{
    Console.WriteLine($"Congratulation for {player.Name}, you're win the game!");
}
