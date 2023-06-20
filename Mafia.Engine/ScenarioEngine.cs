using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;
using Mafia.Engine.Models.Scenarios;

namespace Mafia.Engine;

public class ScenarioEngine
{
    private readonly IScenario _scenario;
    private readonly ScenarioState _state;

    public List<Player> Players { get; set; }
    public List<Round> Rounds { get; set; }
    public Round CurrentRound => Rounds.First(x => x.IsCurrentRound);

    private readonly List<Step> _scenarioStep = new List<Step>();

    public ScenarioEngine(IScenario scenario)
    {
        _scenario = scenario;
        _state = new ScenarioState()
        {
            Stage = StateStage.Morning,
            Command = StateCommand.Ready,
            Audience = StateAudience.All,
            Id = Guid.NewGuid()
        };

        Players = new List<Player>();
        Rounds = new List<Round>()
        {
            new()
            {
                TurnNumber = scenario.HasInterviewDay ? 0 : 1,
                IsInterview = true,
                IsCurrentRound = true,
                RoundPlayers = new List<RoundPlayer>()
            }
        };
    }

    public ScenarioState GetState() => _state;

    public void AddPlayer(Users user)
    {
        Players.Add(new Player()
        {
            User = user,
            TurnNumber = Players.Count + 1,
            Card = null
        });

        Rounds.First().RoundPlayers = Players.Select(x => new RoundPlayer()
        {
            Player = x,
            IsTalked = false
        }).ToList();
    }

    public ScenarioState ShuffleCards()
    {
        var players = Players.ToList();

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Independent))
        {
            var player = players.PickRandom();
            Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Mafia))
        {
            var player = players.PickRandom();
            Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: true }))
        {
            var player = players.PickRandom();
            Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: false }))
        {
            foreach (var player in players)
            {
                Players.First(x => x.TurnNumber == player.TurnNumber).Card = role;
            }
        }

        return _state;
    }

    public ScenarioState Execute(ScenarioState state)
    {
        switch (state.Stage)
        {
            case StateStage.Morning:
                doMorningLogic(state);
                break;
            case StateStage.Day:
                doDayLogic(state);
                break;
            case StateStage.Evening:
                doEveningLogic(state);
                break;
            case StateStage.Night:
                doNightLofic(state);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return _state;
    }

    private void doNightLofic(ScenarioState state)
    {
        if (CurrentRound.IsInterview && state.Command == StateCommand.Ready)
        {
            _state.Stage = StateStage.Night;
            _state.Command = StateCommand.WakeUp;
            _state.Audience = StateAudience.Player;
            _state.CurrentPlayer = GetNextPlayerToWakeUp();
        }
        else if (CurrentRound.IsInterview && state.Command == StateCommand.WakeUp)
        {
            if (!_state.CurrentPlayer!.HasTheLikeShown)
            {
                _state.Stage = StateStage.Night;
                _state.Command = StateCommand.ShowLike;
                _state.Audience = StateAudience.Player;
                _state.CurrentPlayer.HasTheLikeShown = true;
            }
            
        }
        else if (CurrentRound.IsInterview && state.Command == StateCommand.ShowLike)
        {
            if (CurrentRound.IsInterview  && state.CurrentPlayer!.IsAwake)
            {
                _state.Stage = StateStage.Night;
                _state.Command = StateCommand.Sleep;
                _state.Audience = StateAudience.Player;
                _state.CurrentPlayer!.IsAwake = false;
            }
        }
        else if (CurrentRound.IsInterview && state.Command == StateCommand.Sleep)
        {
            _state.Stage = StateStage.Night;
            _state.Command = StateCommand.WakeUp;
            _state.Audience = StateAudience.Player;
            _state.CurrentPlayer = GetNextPlayerToWakeUp();
        }
    }

    private void doEveningLogic(ScenarioState state)
    {
        if (CurrentRound.IsInterview && state.Command == StateCommand.Ready)
        {
            _state.Stage = StateStage.Night;
            _state.Command = StateCommand.Ready;
            _state.Audience = StateAudience.All;
            _state.CurrentPlayer = null;

            SleepAllPlayers();
        }
    }

    private void SleepAllPlayers()
    {
        foreach (var player in Players)
        {
            player.IsAwake = false;
        }
    }

    private void WakeUppAllPlayers()
    {
        foreach (var player in Players)
        {
            player.IsAwake = true;
        }
    }

    private void doDayLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Speak when CurrentRound.IsInterview && !HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Day;
                _state.Command = StateCommand.Speak;
                _state.Audience = StateAudience.Player;
                _state.CurrentPlayer = GetNextPlayerToTalk();
                break;
            case StateCommand.Speak when CurrentRound.IsInterview && HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Evening;
                _state.Command = StateCommand.Ready;
                _state.Audience = StateAudience.All;
                _state.CurrentPlayer = null;
                break;
            default:
                break;
        }
    }

    private void doMorningLogic(ScenarioState state)
    {
        if (CurrentRound.IsInterview && state.Command == StateCommand.Ready)
        {
            _state.Stage = StateStage.Day;
            _state.Command = StateCommand.Speak;
            _state.Audience = StateAudience.Player;
            _state.CurrentPlayer = GetNextPlayerToTalk();
        }
    }

    private Player? GetNextPlayerToTalk()
    {
        var unTalkedPlayers = Players.Where(x => x.HasSpoken == false).OrderBy(x => x.TurnNumber);
        if (_state.CurrentPlayer == null)
        {
            var player = unTalkedPlayers.First();
            player.HasSpoken = true;
            return player;
        }
        else
        {
            var player = unTalkedPlayers.FirstOrDefault(x => x.TurnNumber > _state.CurrentPlayer
                .TurnNumber);

            if (player != null)
            {
                player.HasSpoken = true;
            }

            return player;
        }
    }

    private Player? GetNextPlayerToWakeUp()
    {
        var unAWakedCard = Players.Where(x => !x.IsAwake).Select(x => x.Card).Where(x=>! x!.IsAWakedInNight && x.HasAbility).OrderBy(x => x?.Order);

        var card = unAWakedCard.First();
        card!.IsAWakedInNight = true;
        
        var player = Players.First(x => x.Card?.GetType() == card?.GetType());
        player.IsAwake = true;
        
        return player;
    }

    private bool HaveAllThePlayersSpoken()
    {
        return Players.All(x => x.HasSpoken);
    }
}