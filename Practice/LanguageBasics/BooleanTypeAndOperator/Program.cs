// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dude d1 = new("John");
Dude d2 = new("John");
Console.WriteLine(d1 == d2);

Dude d3 = d1;
Console.WriteLine(d3 == d1);


static bool useUmbrella(bool winds, bool rainy, bool sunny)
{
    return !winds && (rainy || sunny);
}

Console.WriteLine(useUmbrella(true, false, true));

public class Dude()
{
    public string Name = "";

    public Dude(string n) : this()
    {
        Name = n;
    }
}
