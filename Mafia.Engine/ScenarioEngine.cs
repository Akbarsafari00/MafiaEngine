using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;
using Mafia.Engine.Models.Scenarios;

namespace Mafia.Engine;

public class ScenarioEngine
{
    private readonly IScenario _scenario;
    private readonly GameState _state;

    private readonly List<Step> _interviewScenario = new List<Step>();

    public ScenarioEngine(IScenario scenario)
    {
        _scenario = scenario;
        _state = new GameState()
        {
            Players = new List<Player>(),
            Rounds = new List<Round>()
            {
                new()
                {
                    TurnNumber = scenario.hasInterviewDay ? 0 : 1,
                    RoundPlayers = new List<RoundPlayer>()
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

    public void ShuffleCards()
    {
        var players = _state.Players.ToList();

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Independent))
        {
            var player = players.PickRandom();
            _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Mafia))
        {
            var player = players.PickRandom();
            _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: true }))
        {
            var player = players.PickRandom();
            _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: false }))
        {
            foreach (var player in players)
            {
                _state.Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            }
        }
    }

    public void PrepareGame()
    {
        var order = 1;
        if (_scenario.hasInterviewDay)
        {
            foreach (var activity in _scenario.InterViewTemplates)
            {
                switch (activity.Action)
                {
                    case ActivityAction.LoopPlayer:
                    {
                        foreach (var player in _state.Players.OrderBy(x => x.TurnNumber))
                        {
                            activity.SystemAction?.Invoke(_state, player);
                            
                            order++;
                        }

                        break;
                    }
                    case ActivityAction.PlayerFromCard:
                    {
                        
                        var player = _state.Players.First(x => x.Card?.Name == activity?.Card?.Name);
                        
                        activity.SystemAction?.Invoke(_state, player);
                        
                        _interviewScenario.Add(new Step(activity.Audience, StepAction.None, activity.Type,
                            activity.Stage, player.Card, order: order, player: player,
                            isInterViewDay: activity.IsInterViewDay));
                        order++;
                        break;
                    }
                    case ActivityAction.None:
                    default:
                        _interviewScenario.Add(new Step(activity.Audience, StepAction.None, activity.Type,
                            activity.Stage, activity.Card, order: order, isInterViewDay: activity.IsInterViewDay));
                        order++;
                        break;
                }
            }
        }
        
        foreach (var activity in _scenario.RoundTemplates)
        {
            switch (activity.Action)
            {
                case ActivityAction.LoopPlayer:
                {
                    foreach (var player in _state.Players.OrderBy(x => x.TurnNumber))
                    {
                        activity.SystemAction?.Invoke(_state, player);
                        _interviewScenario.Add(new Step(activity.Audience, activity.Action, activity.Type,
                            activity.Stage, player.Card, player, order: order,
                            isInterViewDay: activity.IsInterViewDay));
                        order++;
                    }

                    break;
                }
                case ActivityAction.PlayerFromCard:
                {
                    var player = _state.Players.First(x => x.Card?.Name == activity?.Card?.Name);
                    
                    activity.SystemAction?.Invoke(_state, player);
                    _interviewScenario.Add(new Step(activity.Audience, StepAction.None, activity.Type,
                        activity.Stage, player.Card, order: order, player: player,
                        isInterViewDay: activity.IsInterViewDay));
                    order++;
                    break;
                }
                case ActivityAction.None:
                default:
                    _interviewScenario.Add(new Step(activity.Audience, StepAction.None, activity.Type,
                        activity.Stage, activity.Card, order: order, isInterViewDay: activity.IsInterViewDay));
                    order++;
                    break;
            }
        }
    }

    public GameState Execute(int order = 0)
    {
        _state.Activity =   order == 0 ? _interviewScenario.First() : _interviewScenario.First(x => x.Order > order);
        return _state;
    }
}