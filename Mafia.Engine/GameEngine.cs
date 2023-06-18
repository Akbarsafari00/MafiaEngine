using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class GameEngine
{
    private readonly int _maxPlayer;
    private readonly GameState _state;
    private readonly List<Card> _cards;

    public GameEngine(List<Card> cards, int maxPlayer, bool hasInterviewDay = false, bool freeChallenge = false)
    {
        _cards = cards;
        _maxPlayer = maxPlayer;
        _state = new GameState()
        {
            Action = GameAction.Pending,
          
            CurrentDay = hasInterviewDay ? 0 : 1,
            hasInterviewDay = hasInterviewDay,
            Players = new List<Player>(),
            Rounds = new List<Round>()
            {
                new()
                {
                    NextStage = hasInterviewDay ? GameStage.Day : GameStage.Night,
                    Stage = hasInterviewDay ? GameStage.Day : GameStage.Night,
                    TurnNumber = hasInterviewDay ? 0 : 1,
                }
            },
            Id = Guid.NewGuid()
        };
    }

    public GameState GetState() => _state;

    public void AddPlayer(Users user)
    {
        _state.Players.Add(new Player()
        {
            User = user,
            TurnNumber = _state.Players.Count + 1,
            Card = null
        });

        _state.Rounds.First().RoundPlayers = _state.Players.Select(x => new RoundPlayer()
        {
            Player = x,
            IsTalked = false
        }).ToList();
    }

    public List<Player> Players()
    {
        return _state.Players;
    }

    public void ShuffleCards()
    {
        var players = _state.Players.ToList();

        foreach (var role in _cards.Where(x => x.Side == CardSide.Mafia))
        {
            var player = players.PickRandom();
            _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: true }))
        {
            var player = players.PickRandom();
            _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: false }))
        {
            foreach (var player in players)
            {
                _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            }
        }
    }

    public void Vote(Player player, int voteCount)
    {
        var vote = _state.CurrentRound.RoundPlayers.FirstOrDefault(x => x.Player.TurnNumber == player.TurnNumber);
        if (vote == null)
        {
            throw new Exception("Player Not FOund For Vote");
        }
        else
        {
            vote.VoteCount = 0;
        }
    }

    public GameState Execute()
    {
        if (_state.CurrentRound.RoundPlayers.Count < 0)
        {
            throw new Exception("Not Found Player With Role");
        }
        //
        // if ( _state.CurrentRound.Stage != _state.CurrentRound.NextStage)
        // {
        //     _state.CurrentRound.Stage = _state.CurrentRound.NextStage;
        //     _state.CurrentPlayer = null;
        //     _state.Action = GameAction.Pending;
        // }

        // if (_state.CurrentRound.Stage==GameStage.Day && _state.Action == GameAction.Finished)
        // {
        //     _state.CurrentRound.Stage = GameStage.Evening;
        //     _state.Action = GameAction.Pending;
        // }

        switch (_state.CurrentRound.Stage)
        {
            case GameStage.Morning:
                MorningProcess();
                break;
            case GameStage.Day:
                DayProcess();
                break;
            case GameStage.Evening:
                EveningProcess();
                break;
            case GameStage.Night:
                NightProcess();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return _state;
    }

    private void NightProcess()
    {
        throw new NotImplementedException();
    }

    private void EveningProcess()
    {
        if (_state.Action == GameAction.Pending)
        {
            _state.Action = GameAction.Voting;
            
            _state.CurrentPlayer = _state.CurrentPlayer == null
                ? _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == 1).Player
                : _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == _state.CurrentPlayer.TurnNumber + 1).Player;
        }
        else
        {
            _state.CurrentPlayer = _state.CurrentPlayer == null
                ? _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == 1).Player
                : _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == _state.CurrentPlayer.TurnNumber + 1).Player;
            _state.Action = GameAction.Voting;

            if (IsLastPlayer())
            {
                _state.CurrentRound.NextStage = GameStage.Night;
                _state.Action = GameAction.Pending;
            }
        }
        
       
    }

    private void DayProcess()
    {

       
         if (_state.Action == GameAction.Pending)
        {
            _state.Action = GameAction.Talking;
            _state.CurrentPlayer = _state.CurrentPlayer == null
                ? _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == 1).Player
                : _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == _state.CurrentPlayer.TurnNumber + 1).Player;
        }
        else
        {
            _state.CurrentPlayer = _state.CurrentPlayer == null
                ? _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == 1).Player
                : _state.CurrentRound?.RoundPlayers.First(x => x.Player.TurnNumber == _state.CurrentPlayer.TurnNumber + 1).Player;
            _state.Action = GameAction.Talking;
            
            if (IsLastPlayer())
            {
                _state.Action = GameAction.Finished;
            
            }
        }
    }

    private void MorningProcess()
    {
        throw new NotImplementedException();
    }

    private bool IsLastPlayer()
    {
        return _state.CurrentPlayer != null && _state.CurrentPlayer.TurnNumber == _maxPlayer;
    }
}