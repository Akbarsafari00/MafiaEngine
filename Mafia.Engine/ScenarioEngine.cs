using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;
using Mafia.Engine.Models.Scenarios;

namespace Mafia.Engine;

public class ScenarioEngine
{
    private readonly IScenario _scenario;
    private readonly GameState _state;

    private readonly List<Activity> _interviewScenario = new List<Activity>(); 
    
    public ScenarioEngine(IScenario scenario)
    {
        _scenario = scenario;
        _state = new GameState()
        {
            Action = GameAction.Pending,
          
            CurrentDay = scenario.hasInterviewDay ? 0 : 1,
            hasInterviewDay = scenario.hasInterviewDay,
            Players = new List<Player>(),
            Rounds = new List<Round>()
            {
                new()
                {
                    NextStage = scenario.hasInterviewDay ? GameStage.Day : GameStage.Night,
                    Stage = scenario.hasInterviewDay ? GameStage.Day : GameStage.Night,
                    TurnNumber = scenario.hasInterviewDay ? 0 : 1,
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
        int order = 1;
        foreach (var activity in _scenario.InterViewTemplates)
        {
            if (activity.Action == ActivityAction.ForEachPlayer)
            {
                foreach (var player in _state.Players.OrderBy(x=>x.TurnNumber))
                {
                    _interviewScenario.Add(new Activity(ActivityAction.ForEachPlayer,activity.Type,activity.Stage,activity.Card,player , order:order));
                    order++;
                }
            }
            else
            {
                _interviewScenario.Add(new Activity(ActivityAction.ForSingle,activity.Type,activity.Stage,activity.Card , order: order));
                order++;
            }
        }
    }
    
    public Activity Execute(int order  = 0)
    {
        return order==0 ? _interviewScenario.First() : _interviewScenario.First(x => x.Order > order);
    }

    
}