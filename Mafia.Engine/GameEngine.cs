using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class GameEngine
{
    private readonly int _maxPlayer;
    private readonly GameState _state;
    private readonly List<Player> _players;
    private readonly List<Card> _cards;

    public GameEngine(List<Card> cards,int maxPlayer, bool hasInterviewDay = false)
    {
        _players = new List<Player>();
        _cards = cards;
        _maxPlayer = maxPlayer;
        _state =new GameState()
        {
            Action = GameAction.NotStarted,
            Stage = GameStage.Day,
            CurrentDay = hasInterviewDay?1:0,
            hasInterviewDay = hasInterviewDay,
            Rounds = new List<Round>()
            {
                new Round()
                {
                    Number = hasInterviewDay?1:0,
                    Votes = new List<PlayerVotes>(),
                    NightKills = new List<Player>()
                }
            },
            Id = Guid.NewGuid()
        };
    }

    public GameState GetState() => _state;
    public void AddPlayer(Users user)
    {
        _players.Add(new Player()
        {
            User = user,
            Index = _players.Count + 1,
            Card = null
        });
    }
    

    public void ShuffleRoles()
    {
        var players = _players.ToList();

        foreach (var role in _cards.Where(x => x.Side == CardSide.Mafia))
        {
            var player = players.PickRandom();
            _players.First(x=>x.Index==player.Index).Card = role;
            players.Remove(player);
        }

        foreach (var role in _cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: true }))
        {
            var player = players.PickRandom();
            _players.First(x=>x.Index==player.Index).Card = role;
            players.Remove(player);
        }

        foreach (var role in _cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: false }))
        {
            foreach (var player in players)
            {
                _players.First(x=>x.Index==player.Index).Card = role;
            }
        }
    }

    public void Vote(Player player , int voteCount)
    {
        var vote = _state.CurrentRound.Votes.FirstOrDefault(x => x.Player.Index == player.Index);
        if (vote==null)
        {
            _state.CurrentRound.Votes.Add(new PlayerVotes()
            {
                Player = player,
                Count = voteCount
            });
        }
        else
        {
            vote.Count = voteCount;
        }
    }
    public GameState Execute()
    {
        if (_players.Count < 0)
        {
            throw new Exception("Not Found Player With Role");
        }
        
        if (IsLastPlayer() && _state.Action == GameAction.Talking)
        {
            _state.Stage = GameStage.Voting;
            _state.CurrentPlayer = null;

        }
        else if (IsLastPlayer() && _state.Action == GameAction.Voting)
        {
            _state.Stage = GameStage.Night;
            _state.CurrentPlayer = null;

        }
        
        switch (_state.Stage)
        {
            case GameStage.Day:
            {
                _state.CurrentPlayer = _state.CurrentPlayer==null ? _players.FirstOrDefault(x => x.Index == 1) : _players.FirstOrDefault(x => x.Index == _state.CurrentPlayer.Index + 1);
                _state.Action = GameAction.Talking;
                break;
            }
            case GameStage.Voting:
            {
                _state.CurrentPlayer = _state.CurrentPlayer==null ? _players.FirstOrDefault(x => x.Index == 1) : _players.FirstOrDefault(x => x.Index == _state.CurrentPlayer.Index + 1);
                _state.Action = GameAction.Voting;
                break;
            }
            case GameStage.Night:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return _state;
    }

    private bool IsLastPlayer()
    {
        return _state.CurrentPlayer != null && _state.CurrentPlayer.Index == _maxPlayer;
    }
}