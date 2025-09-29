using ConsoleApp;
using ConsoleUI;
using Controller;
using Enums;
using Interfaces;
using Models;

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
    unoGame.StartGame();

    var UIController = new UIController(unoGame);
    UIController.Run();

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
