namespace MenuSystem;

public class Menu
{
    public string? Title { get; set; }
    public Dictionary<string, MenuItem> MenuItems { get; set; } = [];

    private const string _menuSeparator = "=====================";

    public Menu(string? title, List<MenuItem> menuItems)
    {
        Title = title;

        foreach (var menuItem in menuItems)
        {
            MenuItems[menuItem.Shortcut.ToLower()] = menuItem;
        }
    }

    private void Draw()
    {
        Console.ResetColor();
        if (!string.IsNullOrWhiteSpace(Title))
        {
            Console.WriteLine(Title);
            Console.WriteLine(_menuSeparator);
        }

        foreach (var menuItem in MenuItems)
        {
            Console.WriteLine($"{menuItem.Key}) {menuItem.Value.MenuLabel}");
        }

        Console.WriteLine(_menuSeparator);
        Console.WriteLine("Your choice:");
    }

    public string Run()
    {
        Console.Clear();

        var userChoice = "";

        Console.Clear();
        Draw();
        userChoice = Console.ReadLine()?.Trim();

        if (MenuItems.ContainsKey(userChoice?.ToLower()))
        {
            if (MenuItems[userChoice.ToLower()].MethodToRun != null)
                MenuItems[userChoice.ToLower()].MethodToRun!();
        }
        else
        {
            Console.WriteLine("Undefined shortcut...");
        }

        Console.WriteLine();

        return userChoice;
    }
}