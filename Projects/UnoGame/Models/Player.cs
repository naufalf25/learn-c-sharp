namespace Models;

using Interfaces;

public class Player : IPlayer
{
    public string Name { get; set; }
    public bool HasSaidUno { get; set; }

    public Player(string name)
    {
        Name = name;
        HasSaidUno = false;
    }
}