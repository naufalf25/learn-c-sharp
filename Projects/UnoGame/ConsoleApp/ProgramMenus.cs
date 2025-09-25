using MenuSystem;

namespace ConsoleApp;

public static class ProgramMenus
{
    public static string label = "   \u001b[31m~\u001b[32m~\u001b[33m~\u001b[34m~ U N O \u001b[34m~\u001b[33m~\u001b[32m~\u001b[31m~\u001b[0m   ";

    public static Menu GetMainMenu(Func<string?> startNewGame, Func<string?> endGame) =>
        new(label,
        [
            new MenuItem()
            {
                Shortcut = "s",
                MenuLabel = "Start a new game",
                MethodToRun = startNewGame,
            },
            new MenuItem()
            {
                Shortcut = "x",
                MenuLabel = "Exit game",
                MethodToRun = endGame,
            }
        ]);
}