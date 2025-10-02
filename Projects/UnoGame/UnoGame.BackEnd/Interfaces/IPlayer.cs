using UnoGame.BackEnd.Enums;

namespace UnoGame.BackEnd.Interfaces;

public interface IPlayer
{
    public string Name { get; set; }
    public PlayerType Type { get; set; }
    public bool HasSaidUno { get; set; }
}