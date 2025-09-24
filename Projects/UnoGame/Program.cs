using Controller;
using Enums;
using Interfaces;
using Models;

List<IPlayer> players = new()
{
    new Player("John")
};
IDeck deck = new Deck();
ITable table = new Table();
ICard card = new Card(CardColor.Red, CardNumber.Two, ActionType.Wild);

UnoGameController unoGameController = new(players, deck, table);

unoGameController.StartGame();