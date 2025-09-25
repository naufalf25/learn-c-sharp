namespace Models;

using Enums;
using Interfaces;

public class Player : IPlayer
{
    public string Name { get; set; }
    public PlayerType Type { get; set; }
    public bool HasSaidUno { get; set; }

    public Player(string name, PlayerType type)
    {
        Name = name;
        Type = type;
        HasSaidUno = false;
    }
}