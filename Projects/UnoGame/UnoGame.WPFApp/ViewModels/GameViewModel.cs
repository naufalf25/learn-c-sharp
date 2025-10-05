using System.Collections.ObjectModel;
using System.Windows.Input;
using UnoGame.BackEnd.Controller;
using UnoGame.BackEnd.Interfaces;
using UnoGame.BackEnd.Enums;
using UnoGame.BackEnd.Models;
using UnoGame.WPFApp.Commands;
using System.Windows;
using System.Diagnostics;

namespace UnoGame.WPFApp.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        private readonly MainWindow _mainWindow;
        private readonly UnoGameController _gameController;
        private IPlayer _currentPlayer;
        private CardDisplay _topCard;
        private int _drawCardCount;
        private int _discardPileCount;
        private Visibility _isColorPickerVisibility = Visibility.Hidden;
        private Visibility _centerTable = Visibility.Visible;
        private Visibility _overlayNextPlayer = Visibility.Hidden;
        private Visibility _calledUnoButton = Visibility.Hidden;
        private ICard? _pendingWildCard;
        private bool _isStackedActive;
        private CardColor _currentColor;
        private bool _isChoosingWildColor;

        public ObservableCollection<CardDisplay> PlayerHand { get; } = [];
        public ObservableCollection<string> GameLog { get; } = [];
        public ObservableCollection<PlayerCardInfo> PlayerCards { get; } = [];
        
        public int DrawCardCount
        {
            get => _drawCardCount;
            set
            {
                _drawCardCount = value;
                OnPropertyChanged(nameof(DrawCardCount));
            }
        }
        public IPlayer CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
                LoadPlayerHand();
            }
        }
        public CardDisplay TopCard
        {
            get => _topCard;
            set
            {
                _topCard = value;
                OnPropertyChanged(nameof(TopCard));
            }
        }
        public int DiscardPileCount
        {
            get => _discardPileCount;
            set
            {
                _discardPileCount = value;
                OnPropertyChanged(nameof(DiscardPileCount));
            }
        }
        public Visibility IsColorPickerVisibility
        {
            get => _isColorPickerVisibility;
            set
            {
                _isColorPickerVisibility = value;
                OnPropertyChanged(nameof(IsColorPickerVisibility));
            }
        }
        public Visibility CenterTable
        {
            get => _centerTable;
            set
            {
                _centerTable = value;
                OnPropertyChanged(nameof(CenterTable));
            }
        }
        public bool IsStackedActive
        {
            get => _isStackedActive;
            set
            {
                _isStackedActive = value;
                OnPropertyChanged(nameof(IsStackedActive));
            }
        }
        public CardColor CurrentColor
        {
            get => _currentColor;
            set
            {
                _currentColor = value;
                OnPropertyChanged(nameof(_currentColor));
            }
        }
        public bool IsChoosingWildColor
        {
            get => _isChoosingWildColor;
            set
            {
                _isChoosingWildColor = value;
                OnPropertyChanged(nameof(IsChoosingWildColor));
            }
        }
        public Visibility OverlayNextPlayer
        {
            get => _overlayNextPlayer;
            set
            {
                _overlayNextPlayer = value;
                OnPropertyChanged(nameof(OverlayNextPlayer));
            }
        }
        public Visibility CalledUnoButton
        {
            get => _calledUnoButton;
            set
            {
                _calledUnoButton = value;
                OnPropertyChanged(nameof(CalledUnoButton));
            }
        }

        public ICommand PlayCardCommand { get; }
        public ICommand DrawCardCommand { get; }
        public ICommand PickColorCommand { get; }
        public ICommand ReadyCommand { get; }
        public ICommand TakeDrawCommand { get; }
        public ICommand CallUnoCommand { get; }

        public GameViewModel(MainWindow mainWindow, List<IPlayer> players)
        {
            _mainWindow = mainWindow;

            var deck = DeckBuilder.BuildDeck();
            var table = new Table();
            _gameController = new UnoGameController(players, deck, table);
            _gameController.StartGame();

            CurrentPlayer = _gameController.GetCurrentPlayer();
            LoadPlayerHand();
            UpdatePlayerCards();
            var topCard = _gameController.GetTopCardFromTable();
            TopCard = new CardDisplay(topCard, GetCardImagePath(topCard), false);
            CurrentColor = _gameController.DeclaredCardColor();
            DrawCardCount = _gameController.GetRemainingCardsInDeck();
            DiscardPileCount = _gameController.GetCardsOnTable();

            _gameController.OnGameAction += (string payload) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    GameLog.Add($"[{DateTime.Now:T}] {payload}");
                });
            };
            _gameController.OnPlayerWin += (IPlayer player) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    _mainWindow.NavigateToEndGame(player);
                });
            };

            PlayCardCommand = new RelayCommand(cardObj => {
                if (cardObj is ICard card)
                {
                    var match = PlayerHand.FirstOrDefault(c => c.Card == card);
                    if (match != null)
                    {
                        if (card.Action == ActionType.Wild || card.Action == ActionType.WildDrawFour)
                        {
                            _pendingWildCard = card;
                            CenterTable = Visibility.Hidden;
                            IsColorPickerVisibility = Visibility.Visible;
                            OnPropertyChanged(nameof(IsColorPickerVisibility));
                            OnPropertyChanged(nameof(CenterTable));
                            return;
                        }

                        if (_isColorPickerVisibility == Visibility.Hidden)
                            PlayCard(card);
                    }
                }
            });

            DrawCardCommand = new RelayCommand(_ => {
                _gameController.DrawCard(CurrentPlayer);
                DrawCardCount = _gameController.GetRemainingCardsInDeck();
                LoadPlayerHand();
                UpdatePlayerCards();
                OnPropertyChanged(nameof(DrawCardCount));
            });

            PickColorCommand = new RelayCommand(colorObj =>
            {
                if (colorObj is string colorName && Enum.TryParse<CardColor>(colorName, true, out CardColor color))
                {
                    if (_pendingWildCard != null)
                    {
                        _gameController.DeclareWildColor(color);
                        PlayCard(_pendingWildCard);
                        _pendingWildCard = null;
                        OverlayNextPlayer = Visibility.Visible;
                        IsColorPickerVisibility = Visibility.Hidden;
                        CenterTable = Visibility.Visible;
                        OnPropertyChanged(nameof(OverlayNextPlayer));
                        OnPropertyChanged(nameof(IsColorPickerVisibility));
                        OnPropertyChanged(nameof(CenterTable));
                    }
                }
            });

            ReadyCommand = new RelayCommand(_ =>
            {
                OverlayNextPlayer = Visibility.Hidden;
            });

            TakeDrawCommand = new RelayCommand(_ =>
            {
                GameLog.Add($"[{DateTime.Now:T}] {CurrentPlayer.Name} choose to take the draw instead of countering");
                _gameController.HandleStackableDraw(null, CurrentPlayer, false);
                IsStackedActive = false;

                DrawCardCount = _gameController.GetRemainingCardsInDeck();
                LoadPlayerHand();
                OnPropertyChanged(nameof(DrawCardCount));
            });

            CallUnoCommand = new RelayCommand(_ =>
            {
                if (_gameController.GetPlayerHand(CurrentPlayer).Count == 1)
                {
                    _gameController.CallUno(CurrentPlayer);
                }
            });
        }
        
        private void PlayCard(ICard card)
        {
            if (_gameController.PlayCard(CurrentPlayer, card))
            {
                if (_gameController.GetPlayerHand(CurrentPlayer).Count == 0)
                    return;
                CurrentPlayer = _gameController.GetCurrentPlayer();
                var topCard = _gameController.GetTopCardFromTable();
                TopCard = new CardDisplay(topCard, GetCardImagePath(topCard), false);
                DiscardPileCount = _gameController.GetCardsOnTable();
                _currentColor = _gameController.DeclaredCardColor();
                IsStackedActive = _gameController.IsStackedActive;
                CenterTable = Visibility.Visible;
                OverlayNextPlayer = Visibility.Visible;
                OnPropertyChanged(nameof(OverlayNextPlayer));
                OnPropertyChanged(nameof(CurrentPlayer));
                OnPropertyChanged(nameof(TopCard));
                OnPropertyChanged(nameof(DiscardPileCount));
                OnPropertyChanged(nameof(CurrentColor));
                OnPropertyChanged(nameof(CenterTable));
                OnPropertyChanged(nameof(IsStackedActive));

                HandleStackedCard();
                UpdatePlayerCards();
            }
        }

        private void HandleStackedCard()
        {
            if (_gameController.IsStackedActive)
            {
                var matchingCard = _gameController.MatchingStackedCard(CurrentPlayer);
                if (matchingCard != null)
                {
                    IsStackedActive = true;
                    GameLog.Add($"[{DateTime.Now:T}] You can counter the {_gameController.StackedCardAction}");
                }
                else
                {
                    _gameController.HandleStackableDraw(null, CurrentPlayer, true);
                    IsStackedActive = false;

                    LoadPlayerHand();
                    DrawCardCount = _gameController.GetRemainingCardsInDeck();
                    OnPropertyChanged(nameof(DrawCardCount));
                }
                
            }
        }

        private void LoadPlayerHand()
        {
            PlayerHand.Clear();
            foreach (var card in _gameController.GetPlayerHand(CurrentPlayer))
            {
                bool canPlay = false;
                if (_gameController.IsStackedActive)
                {
                    canPlay = card.Action == _gameController.StackedCardAction;
                }
                else
                {
                    canPlay = _gameController.CanPlayCard(CurrentPlayer, card);
                }
                    
                PlayerHand.Add(new CardDisplay(card, GetCardImagePath(card), canPlay));
            }

            if (_gameController.GetPlayerHand(CurrentPlayer).Count == 1)
                CalledUnoButton = Visibility.Visible;
            else
                CalledUnoButton = Visibility.Hidden;

            OnPropertyChanged(nameof(PlayerHand));
            OnPropertyChanged(nameof(CalledUnoButton));
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

        public class PlayerCardInfo
        {
            public string Name { get; set; }
            public int CardCount { get; set; }
        }

        private static string GetCardImagePath(ICard card)
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

        private void UpdatePlayerCards()
        {
            PlayerCards.Clear();
            var players = _gameController.GetAllPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == CurrentPlayer) continue;
                PlayerCards.Add(new PlayerCardInfo
                {
                    Name = $"Player {i + 1} - {players[i].Name}",
                    CardCount = _gameController.GetPlayerHand(players[i]).Count,
                });
            }
            OnPropertyChanged(nameof(PlayerCards));
        }
    }
}
