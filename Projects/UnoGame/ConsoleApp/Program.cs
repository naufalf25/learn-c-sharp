using ConsoleApp;
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
    unoGame.StartGame();

    return null;
}

string? ExitGame()
{
    return null;
}
