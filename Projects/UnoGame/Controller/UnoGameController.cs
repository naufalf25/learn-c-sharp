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
            if (color == CardColor.Wild) break;

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
            _deck.Cards.Add(new Card(CardColor.Wild, null, ActionType.Wild));
            _deck.Cards.Add(new Card(CardColor.Wild, null, ActionType.WildDrawFour));
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
                    _deck.Cards.Remove(card);
                    AddCardToPlayer(player, card);
                }
            }
        }

        while (!_deck.IsEmpty && _table.DiscardCount == 0)
        {
            var firstCard = _deck.Cards.First();
            _deck.Cards.Remove(firstCard);

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
        OnGameAction?.Invoke($"Current Player: {GetCurrentPlayer().Name}\n");
    }

    public void EndGame()
    {
        _gameEnded = true;
    }

    public bool PlayCard(IPlayer player, ICard card)
    {
        if (!CanPlayCard(player, card))
        {
            OnGameAction?.Invoke($"Invalid move by {player.Name} | Can't play card {card.Color} - {(card.Action.HasValue ? card.Action : card.Number)}");
            return false;
        }

        var cardsToPlay = CardsToPlay(player, card);

        foreach (var playedCard in cardsToPlay)
        {
            RemoveCardFromPlayer(player, playedCard);
            _table.DiscardPile.Add(playedCard);
            _table.DiscardCount++;
            OnCardPlayed?.Invoke(player, playedCard);
        }

        if (!card.IsWild && card.Color != _table.TopCard.Color)
            DeclareWildColor(card.Color);

        _table.TopCard = card;
        ProcessActionCard(card);

        if (_playerhands[player].Count == 0 && !player.HasSaidUno)
        {
            OnGameAction?.Invoke($"{player.Name} not said UNO before and forced to draw two cards");
            DrawCardCount(player, true, 2);
        }

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
        _deck.Cards.Remove(drawnCard);
        AddCardToPlayer(player, drawnCard);

        if (_deck.Cards.Count == 0)
            _deck.IsEmpty = true;

        OnGameAction?.Invoke(isForced
            ? $"{player.Name} was forced to draw a card"
            : $"{player.Name} drew a card");

        return drawnCard;
    }

    public bool CanPlayCard(IPlayer player, ICard card)
    {
        if (!_playerhands[player].Contains(card)) return false;

        if (GetCurrentPlayer() != player) return false;

        var topCard = GetTopCardFromTable();
        if (topCard == null) return true;

        if (card.IsWild) return true;

        if (card.Number.HasValue && card.Number == topCard.Number) return true;

        if (card.Action == ActionType.DrawTwo || card.Action == ActionType.WildDrawFour && topCard.Action == ActionType.DrawTwo || topCard.Action == ActionType.WildDrawFour)
            return true;

        if (card.Action.HasValue && card.Action == topCard.Action) return true;

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

    private List<ICard> CardsToPlay(IPlayer player, ICard card)
    {
        var duplicates = _playerhands[player].Where(c => c != card && c.Color == card.Color && c.Number == card.Number && card.Number.HasValue).ToList();
        var cardsToPlay = new List<ICard> { card };

        if (duplicates.Count != 0)
        {
            foreach (var duplicateCard in duplicates)
            {
                cardsToPlay.Add(duplicateCard);
                OnGameAction?.Invoke($"{player.Name} perform multi card play | {card.DisplayName}");
            }
            ;
        }

        return cardsToPlay;
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
        _declaredWildColor = topCard.Color;

        var shuffled = cardToReshuffle.OrderBy(x => _random.Next()).ToList();
        foreach (var card in shuffled)
        {
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
        _declaredWildColor = card.Color;
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
            if (_currentPlayerIndex == 0)
                _currentPlayerIndex = totalPlayer - 1;
            else
                _currentPlayerIndex--;
        }
        else
        {
            if (_currentPlayerIndex == totalPlayer - 1)
                _currentPlayerIndex = 0;
            else
                _currentPlayerIndex++;
        }
    }

    private void ProcessActionCard(ICard card)
    {
        if (card.Action == ActionType.Skip)
            ExecuteSkip();
        else if (card.Action == ActionType.Reverse)
            ExecuteReverse();
        else if (card.Action == ActionType.DrawTwo)
        {
            NextPlayer();
            if (!HandleStackableDraw(GetCurrentPlayer(), (ActionType)card.Action))
            {
                ExecuteDrawTwo();
            }
        }
        else if (card.Action == ActionType.Wild)
            ExecuteWild();
        else if (card.Action == ActionType.WildDrawFour)
        {
            NextPlayer();
            if (!HandleStackableDraw(GetCurrentPlayer(), (ActionType)card.Action))
            {
                ExecuteWildDrawFour();
            }
        }
        else
            NextPlayer();
    }


    private void ExecuteSkip()
    {
        NextPlayer();
        OnGameAction?.Invoke($"{GetCurrentPlayer().Name} was skipped!");
        NextPlayer();
    }

    private void ExecuteReverse()
    {
        NextPlayer();
        _isReversed = !_isReversed;

        OnGameAction?.Invoke("Direction changed!");
        NextPlayer();
    }

    private void ExecuteDrawTwo()
    {
        var targetPlayer = GetCurrentPlayer();

        DrawCardCount(targetPlayer, true, 2);

        OnGameAction?.Invoke($"{targetPlayer.Name} draws 2 cards an loses their turn");
        NextPlayer();
    }

    private void ExecuteWild()
    {
        OnGameAction?.Invoke($"Change Card Color on Next Turn");
        NextPlayer();
    }

    private void ExecuteWildDrawFour()
    {
        var targetPlayer = GetCurrentPlayer();

        DrawCardCount(targetPlayer, true, 4);

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

    public int GetRemainingCardsInDeck()
    {
        return _deck.Cards.Count;
    }

    public int GetCardsOnTable()
    {
        return _table.DiscardCount;
    }

    public bool IsReversed()
    {
        return _isReversed;
    }

    public CardColor DeclaredCardColor()
    {
        return (CardColor)_declaredWildColor;
    }

    public bool CallUno(IPlayer player)
    {
        if (_playerhands[player].Count != 1)
        {
            OnGameAction?.Invoke($"{player.Name} call UNO on wrong time");

            DrawCardCount(player, true, 2);
            return false;
        }

        var getPlayer = _players.FirstOrDefault(p => p == player);
        if (getPlayer != null) getPlayer.HasSaidUno = true;

        OnGameAction?.Invoke($"{player.Name} call UNO!");
        return true;
    }

    private void DrawCardCount(IPlayer player, bool isForced, int count)
    {
        for (int i = 0; i < count; i++)
        {
            DrawCard(player, isForced);
        }
    }

    private bool HandleStackableDraw(IPlayer player, ActionType action)
    {
        var mathingCard = _playerhands[player].FirstOrDefault(c => c.Action == ActionType.DrawTwo || c.Action == ActionType.WildDrawFour);

        if (mathingCard != null)
        {
            OnGameAction?.Invoke($"{player.Name} counters with another {action}");
            PlayCard(player, mathingCard);
            return true;
        }

        return false;
    }
}