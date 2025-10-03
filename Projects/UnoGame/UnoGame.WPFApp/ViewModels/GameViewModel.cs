using System.Collections.ObjectModel;
using System.Windows.Input;
using UnoGame.BackEnd.Controller;
using UnoGame.BackEnd.Interfaces;
using UnoGame.BackEnd.Enums;
using UnoGame.BackEnd.Models;
using UnoGame.WPFApp.Commands;

namespace UnoGame.WPFApp.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;
        private readonly UnoGameController _gameController;

        public IPlayer CurrentPlayer { get; }
        public ObservableCollection<CardDisplay> PlayerHand { get; } = [];
        public ICard TopCard { get; }

        public ICommand PlayCardCommand { get; }
        public ICommand DrawCardCommand { get; }

        public GameViewModel(MainWindow mainWindow, List<IPlayer> players)
        {
            _mainWindow = mainWindow;

            var deck = DeckBuilder.BuildDeck();
            var table = new Table();
            _gameController = new UnoGameController(players, deck, table);
            _gameController.StartGame();

            CurrentPlayer = _gameController.GetCurrentPlayer();
            LoadPlayerHand();
            TopCard = _gameController.GetTopCardFromTable();

            PlayCardCommand = new RelayCommand(cardObj => {
                if (cardObj is CardDisplay cardDisplay)
                {
                    if (PlayerHand.Contains(cardDisplay))
                    {
                        bool played = _gameController.PlayCard(CurrentPlayer, cardDisplay.Card);
                        if (played)
                        {
                            LoadPlayerHand();
                            OnPropertyChanged(nameof(CurrentPlayer));
                            OnPropertyChanged(nameof(TopCard));
                        }
                    }
                }
            });

            DrawCardCommand = new RelayCommand(_ => {
                _gameController.DrawCard(CurrentPlayer);
                OnPropertyChanged(nameof(PlayerHand));
            });
        }

        private void LoadPlayerHand()
        {
            PlayerHand.Clear();
            foreach (var card in _gameController.GetPlayerHand(CurrentPlayer))
            {
                var canPlay = _gameController.CanPlayCard(CurrentPlayer, card);
                PlayerHand.Add(new CardDisplay(card, GetCardImagePath(card), canPlay));
            }
        }

        public class CardDisplay
        {
            public ICard Card { get; }
            public string ImagePath { get; }
            public bool CanPlay { get; }

            public CardDisplay(ICard card, string imagePath, bool canPlay)
            {
                Card = card;
                ImagePath = imagePath;
                CanPlay = canPlay;
            }
        }

        private string GetCardImagePath(ICard card)
        {
            string colorCode = card.Color switch
            {
                CardColor.Red => "R",
                CardColor.Yellow => "Y",
                CardColor.Green => "G",
                CardColor.Blue => "B",
                _ => ""
            };

            string code = card.Action switch
            {
                ActionType.Skip => $"{colorCode}S",
                ActionType.Reverse => $"{colorCode}R",
                ActionType.DrawTwo => $"{colorCode}+2",
                ActionType.Wild => "W",
                ActionType.WildDrawFour => "W+4",
                _ => card.Number.HasValue ? $"{colorCode}{(int)card.Number}" : ""
            };

            return $"pack://application:,,,/Resources/Card/{code}.png";
        }
    }
}
