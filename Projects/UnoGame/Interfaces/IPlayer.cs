using Enums;

namespace Interfaces;

public interface IPlayer
{
    public string Name { get; set; }
    public PlayerType Type { get; set; }
    public bool HasSaidUno { get; set; }
}