namespace Controller;

using Enums;
using Interfaces;
using Models;

public class UnoGameController
{
    private Dictionary<IPlayer, List<ICard>> _playerhands = [];
    private List<IPlayer> _players = [];
    private IDeck _deck;
    private ITable _table;
    private int _currentPlayerIndex;
    private bool _isReversed;
    private bool _gameEnded;
    private IPlayer? _winner;
    private CardColor? _declaredWildColor;
    private Random _random;
    public Action<string> OnGameAction;
    public Action<IPlayer, ICard> OnCardPlayed;
    public Action<IPlayer> OnPlayerWin;

    public UnoGameController(List<IPlayer> players, IDeck deck, ITable table)
    {
        _players = players;
        _deck = deck;
        _table = table;
        _currentPlayerIndex = 0;
        _isReversed = false;
        _gameEnded = false;
        _declaredWildColor = null;
        _random = new Random();
        _winner = null;
    }

    private void InitializeGame()
    {
        _playerhands.Clear();
        _table.DiscardPile.Clear();
        _table.DiscardCount = 0;
        InitializeDeckWithAllCards();

        foreach (var player in _players)
        {
            _playerhands[player] = new List<ICard>();
        }

        if (_deck.Cards.Count > 0)
        {
            var shuffledCard = _deck.Cards.OrderBy(x => _random.Next()).ToList();
            _deck.Cards.Clear();

            foreach (var card in shuffledCard)
            {
                _deck.Cards.Add(card);
            }
        }

        _currentPlayerIndex = 0;
        _isReversed = false;
        _declaredWildColor = null;
        _gameEnded = false;
        _winner = null;
        _deck.IsEmpty = _deck.Cards.Count == 0;
    }

    private void InitializeDeckWithAllCards()
    {
        _deck.Cards.Clear();

        foreach (CardColor color in Enum.GetValues<CardColor>())
        {
            _deck.Cards.Add(new Card(color, CardNumber.Zero, null));

            for (int i = 1; i <= 9; i++)
            {
                var cardNumber = (CardNumber)i;
                _deck.Cards.Add(new Card(color, cardNumber, null));
                _deck.Cards.Add(new Card(color, cardNumber, null));
            }

            for (int i = 0; i < 2; i++)
            {
                _deck.Cards.Add(new Card(color, null, ActionType.Skip));
                _deck.Cards.Add(new Card(color, null, ActionType.Reverse));
                _deck.Cards.Add(new Card(color, null, ActionType.DrawTwo));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            _deck.Cards.Add(new Card(null, null, ActionType.Wild));
            _deck.Cards.Add(new Card(null, null, ActionType.WildDrawFour));
        }
    }

    private void DealInitialCard()
    {
        int initialCardCount = 7;

        for (int i = 0; i < initialCardCount; i++)
        {
            foreach (var player in _players)
            {
                if (!_deck.IsEmpty)
                {
                    var card = _deck.Cards.First();
                    _deck.Cards.RemoveAt(0);
                    AddCardToPlayer(player, card);
                }
            }
        }

        while (!_deck.IsEmpty && _table.DiscardCount == 0)
        {
            var firstCard = _deck.Cards.First();
            _deck.Cards.RemoveAt(0);

            if (firstCard.Action == null)
            {
                InitializeTableWithCard(firstCard);
                break;
            }
            else
            {
                _deck.Cards.Add(firstCard);
            }
        }
    }

    public void StartGame()
    {
        InitializeGame();
        DealInitialCard();

        OnGameAction?.Invoke("Game Started!");
        OnGameAction?.Invoke($"Current Player: {GetCurrentPlayer().Name}");
    }

    public bool PlayCard(IPlayer player, ICard card)
    {
        if (!CanPlayCard(player, card))
        {
            OnGameAction?.Invoke($"Invalid move by {player.Name}");
            return false;
        }

        RemoveCardFromPlayer(player, card);

        _table.DiscardPile.Add(card);
        _table.DiscardCount++;

        OnGameAction?.Invoke($"Player {player.Name} played card {card.DisplayName}");

        ProcessActionCard(card);

        if (CheckPlayerHasWon(player))
        {
            _winner = player;
            _gameEnded = true;
            OnPlayerWin?.Invoke(player);
            OnGameAction?.Invoke($"Player {player.Name} wins the game!");
            return true;
        }

        OnGameAction?.Invoke($"Current player: {GetCurrentPlayer().Name}");

        return true;
    }

    public ICard DrawCard(IPlayer player, bool isForced = false)
    {
        if (_deck.IsEmpty)
        {
            ReshuffleDiscardPileToDeck();

            OnGameAction?.Invoke("No more card available");
            return null;
        }

        var drawnCard = _deck.Cards.First();
        _deck.Cards.RemoveAt(0);
        AddCardToPlayer(player, drawnCard);

        if (_deck.Cards.Count == 0)
            _deck.IsEmpty = true;

        OnGameAction?.Invoke($"{player.Name} drew a card");

        return drawnCard;
    }

    public bool CanPlayCard(IPlayer player, ICard card)
    {
        if (!_playerhands[player].Contains(card)) return false;

        if (GetCurrentPlayer() != player) return false;

        var topCard = GetTopCardFromTable();
        if (topCard == null) return true;

        if (card.IsWild) return true;

        var effectiveTopColor = _declaredWildColor ?? topCard.Color;
        if (card.Color == effectiveTopColor) return true;

        return false;
    }

    public List<ICard> GetPlayerHand(IPlayer player)
    {
        return _playerhands.ContainsKey(player) ? _playerhands[player] : [];
    }

    public void AddCardToPlayer(IPlayer player, ICard card)
    {
        if (!_playerhands.ContainsKey(player))
            _playerhands[player] = [];

        _playerhands[player].Add(card);
    }

    public bool RemoveCardFromPlayer(IPlayer player, ICard card)
    {
        return _playerhands[player].Remove(card);
    }

    private bool CheckPlayerHasWon(IPlayer player)
    {
        return _playerhands.ContainsKey(player) && _playerhands[player].Count == 0;
    }

    public void ReshuffleDiscardPileToDeck()
    {
        if (_table.DiscardCount <= 1) return;

        var topCard = _table.DiscardPile.Last();
        var cardToReshuffle = _table.DiscardPile.Take(_table.DiscardCount - 1).ToList();

        _table.DiscardPile.Clear();
        _table.DiscardPile.Add(topCard);
        _table.TopCard = topCard;

        var shuffled = cardToReshuffle.OrderBy(x => _random.Next()).ToList();
        foreach (var card in shuffled)
        {
            if (card.IsWild)
                _declaredWildColor = null;

            _deck.Cards.Add(card);
        }

        OnGameAction?.Invoke("Discard pile reshuffled into deck");
    }

    public ICard GetTopCardFromTable()
    {
        return _table.TopCard;
    }

    public void InitializeTableWithCard(ICard card)
    {
        _table.DiscardPile.Clear();
        _table.DiscardPile.Add(card);
        _table.DiscardCount++;

        _table.TopCard = card;
    }

    public IPlayer GetCurrentPlayer()
    {
        return _players[_currentPlayerIndex];
    }

    private void NextPlayer()
    {
        int totalPlayer = _players.Count;

        if (_isReversed)
        {
            _currentPlayerIndex--;
            if (_currentPlayerIndex < 0)
                _currentPlayerIndex = totalPlayer;
        }
        else
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex > totalPlayer)
                _currentPlayerIndex = 0;
        }
    }

    private void ProcessActionCard(ICard card)
    {
        if (card.Action == ActionType.Skip)
            ExecuteSkip();
        else if (card.Action == ActionType.Reverse)
            ExecuteReverse();
        else if (card.Action == ActionType.DrawTwo)
            ExecuteDrawTwo();
        else if (card.Action == ActionType.Wild)
            ExecuteWild();
        else if (card.Action == ActionType.WildDrawFour)
            ExecuteWildDrawFour();

        if (card.IsWild)
        {
            DeclareWildColor(card.Color);
        }
    }

    private void ExecuteSkip()
    {
        NextPlayer();
        OnGameAction?.Invoke($"{GetCurrentPlayer().Name} was skipped!");
        NextPlayer();
    }

    private void ExecuteReverse()
    {
        _isReversed = !_isReversed;
        NextPlayer();
        OnGameAction?.Invoke("Direction changed!");
    }

    private void ExecuteDrawTwo()
    {
        NextPlayer();
        var targetPlayer = GetCurrentPlayer();

        for (int i = 0; i < 2; i++)
        {
            DrawCard(targetPlayer, true);
        }

        OnGameAction?.Invoke($"{targetPlayer.Name} draws 2 cards an loses their turn");
        NextPlayer();
    }

    private void ExecuteWild()
    {
        NextPlayer();
    }

    private void ExecuteWildDrawFour()
    {
        NextPlayer();

        var targetPlayer = GetCurrentPlayer();

        for (int i = 0; i < 4; i++)
        {
            DrawCard(targetPlayer, true);
        }

        OnGameAction?.Invoke($"{targetPlayer.Name} draws 4 cards and loses their turn");
        NextPlayer();
    }

    public void DeclareWildColor(CardColor? color)
    {
        _declaredWildColor = color;
        OnGameAction?.Invoke($"Wild color declared: {color}");
    }

    public string GetGameStatus()
    {
        if (_gameEnded)
            return "Ended";

        if (!_deck.IsEmpty)
            return "In Progress";

        return "Not Started";
    }

    public List<IPlayer> GetAllPlayers()
    {
        return new List<IPlayer>(_players);
    }
}