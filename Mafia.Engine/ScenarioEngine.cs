using Mafia.Engine.Extensions;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;
using Mafia.Engine.Models.Extensions;
using Mafia.Engine.Models.Scenarios;

namespace Mafia.Engine;

public class ScenarioEngine
{
    private readonly IScenario _scenario;
    private readonly ScenarioState _state;
    

    private readonly List<Step> _scenarioStep = new List<Step>();

    public ScenarioEngine(IScenario scenario)
    {
        _scenario = scenario;
        _state = new ScenarioState()
        {
            Stage = StateStage.Morning,
            Command = StateCommand.Ready,
            Audience = StateAudience.All,
            CurrentPlayers = new GamePlayers(),
            GameRounds = new GameRounds()
            {
                new()
                {
                    TurnNumber = scenario.HasInterviewDay ? 0 : 1,
                    IsInterview = true,
                    IsCurrentRound = true,
                    RoundPlayers = new GameRoundPlayers()
                }
            },
            Id = Guid.NewGuid()
        };
        
    }

    public void AddPlayer(Users user)
    {
        _state.GameRounds.CurrentRound().RoundPlayers.Add(new RoundPlayer()
        {
            Player = new Player()
            {
                User = user,
                TurnNumber = _state.GameRounds.CurrentRound().RoundPlayers.Count + 1,
                Card = new CitizenCard(0),
            },
            IsTalked = false,
            HasVoteTaken = false,
            VoteCount = 0
        });
    }

    public ScenarioState ShuffleCards()
    {
        var players = _state.GameRounds.CurrentRound().RoundPlayers;

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Independent))
        {
            var player = players.PickRandom();
            _state.GameRounds.CurrentRound().RoundPlayers.First(x => x.Player.TurnNumber == player.Player.TurnNumber).Player.Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x.Side == CardSide.Mafia))
        {
            var player = players.PickRandom();
            _state.GameRounds.CurrentRound().RoundPlayers.First(x => x.Player.TurnNumber == player.Player.TurnNumber).Player.Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: true }))
        {
            var player = players.PickRandom();
            _state.GameRounds.CurrentRound().RoundPlayers.First(x => x.Player.TurnNumber == player.Player.TurnNumber).Player.Card = role;
            players.Remove(player);
        }

        foreach (var role in _scenario.Cards.Where(x => x is { Side: CardSide.Citizen, HasAbility: false }))
        {
            foreach (var player in players)
            {
                _state.GameRounds.CurrentRound().RoundPlayers.First(x => x.Player.TurnNumber == player.Player.TurnNumber).Player.Card = role;
            }
        }

        return _state;
    }

    public ScenarioState Execute(ScenarioState state)
    {
        if (_state.GameRounds.CurrentRound().IsInterview)
        {
            InterviewScenarioHandler(state);
        }
        else
        {
            RoundScenarioHandler(state);
        }

        return _state;
    }

    private void RoundScenarioHandler(ScenarioState state)
    {
        switch (state.Stage)
        {
            case StateStage.Morning:
                DoRoundMorningLogic(state);
                break;
            case StateStage.Day:
                DoRoundDayLogic(state);
                break;
            case StateStage.Evening:
                DoRoundEveningLogic(state);
                break;
            case StateStage.Night:
                DoRoundNightLogic(state);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoRoundNightLogic(ScenarioState state)
    {
        throw new NotImplementedException();
    }

    private void DoRoundEveningLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Ready when state.Audience == StateAudience.All:
                _state.Stage = StateStage.Evening;
                _state.Command = StateCommand.Vote;
                _state.Audience = StateAudience.Player;
                SetCurrentPlayers(GetNextPlayerToVote()!);
                break;
            case StateCommand.Ready:
                break;
            case StateCommand.Speak:
                break;
            case StateCommand.Vote when state.Audience== StateAudience.Player:
                _state.Stage = StateStage.Evening;
                _state.Command = StateCommand.Vote;
                _state.Audience = StateAudience.Player;
                SetCurrentPlayers(GetNextPlayerToVote()!);
                break;
            case StateCommand.WakeUp:
                break;
            case StateCommand.Sleep:
                break;
            case StateCommand.NightAct:
                break;
            case StateCommand.ShowLike:
                break;
            case StateCommand.GoodMorning:
                break;
            case StateCommand.Defending:
                break;
            case StateCommand.DefendingVote:
                break;
            case StateCommand.LastChangeCard:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoRoundDayLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Speak  when !HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Day;
                _state.Command = StateCommand.Speak;
                _state.Audience = StateAudience.Player;
                SetCurrentPlayers(GetNextPlayerToTalk()!);
                break;
            case StateCommand.Speak  when HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Evening;
                _state.Command = StateCommand.Ready;
                _state.Audience = StateAudience.All;
                _state.CurrentPlayers.Clear();
                break;
            case StateCommand.Ready:
                break;
            case StateCommand.Speak:
                break;
            case StateCommand.Vote:
                break;
            case StateCommand.WakeUp:
                break;
            case StateCommand.Sleep:
                break;
            case StateCommand.NightAct:
                break;
            case StateCommand.ShowLike:
                break;
            case StateCommand.GoodMorning:
                break;
            case StateCommand.Defending:
                break;
            case StateCommand.DefendingVote:
                break;
            case StateCommand.LastChangeCard:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoRoundMorningLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Ready when state.Audience==StateAudience.All:
                _state.Stage = StateStage.Day;
                _state.Command = StateCommand.Speak;
                _state.Audience = StateAudience.Player;
                SetCurrentPlayers(GetNextPlayerToTalk()!);
                break;
            case StateCommand.Ready:
                break;
            case StateCommand.Speak:
                break;
            case StateCommand.Vote:
                break;
            case StateCommand.WakeUp:
                break;
            case StateCommand.Sleep:
                break;
            case StateCommand.NightAct:
                break;
            case StateCommand.ShowLike:
                break;
            case StateCommand.GoodMorning:
                break;
            case StateCommand.Defending:
                break;
            case StateCommand.DefendingVote:
                break;
            case StateCommand.LastChangeCard:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void InterviewScenarioHandler(ScenarioState state)
    {
        switch (state.Stage)
        {
            case StateStage.Morning:
                DoInterviewMorningLogic(state);
                break;
            case StateStage.Day:
                DoInterviewDayLogic(state);
                break;
            case StateStage.Evening:
                DoInterviewEveningLogic(state);
                break;
            case StateStage.Night:
                DoInterviewNightLogic(state);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void DoInterviewNightLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Ready
                when state.Audience == StateAudience.All:
                SleepPlayers(GamePlayers);
                break;
            
            case StateCommand.Sleep
                when _state.CurrentPlayers.IsAnyPlayerWakeUpWithMafia() &&
                     GamePlayers.IsAnyPlayerWakeUpWithMafia(x => x.IsAwake):
                SleepPlayers(GetNextPlayerToSleep());
                break;
            
            case StateCommand.Sleep
                when GamePlayers.IsAllPlayerSleep() &&
                     GamePlayers.IsAllAbilityPlayersAWakedUpInNight():
                WakeUpPlayers(GamePlayers);
                break;
            
            case  StateCommand.Sleep when state.Audience == StateAudience.Player:
                _state.Stage = StateStage.Night;
                WakeUpPlayers(GetNextPlayerToWakeUp());
                break;
            
            case StateCommand.Sleep:
                WakeUpPlayers(GetNextPlayerToWakeUp());
                break;
            
            case StateCommand.WakeUp
                when _state.CurrentPlayers.IsAnyPlayerWakeUpWithMafia()
                     && GamePlayers.IsAnyPlayerWakeUpWithMafia(x => !x.Card!.IsAWakedInNight):
                WakeUpPlayers(GetNextPlayerToWakeUp());
                break;
            
            case StateCommand.WakeUp when _state.Audience == StateAudience.Player:
                ShowLikePlayers(GetShowLikePlayers());
                break;
            
            case StateCommand.WakeUp
                when _state.Audience == StateAudience.All && GamePlayers.IsAllAbilityPlayersAWakedUpInNight():
                ReadyToNewMorning();
                break;
            
            case StateCommand.ShowLike
                when _state.CurrentPlayers.IsAnyPlayerWakeUpWithMafia() &&
                     GamePlayers.IsAnyPlayerWakeUpWithMafia(x => !x.HasTheLikeShown):
                ShowLikePlayers(GetShowLikePlayers());
                break;

            case StateCommand.ShowLike
                when _state.CurrentPlayers.Any(x => x.Card.HasInterViewAct):
                _state.Audience = StateAudience.Player;
                _state.Command = StateCommand.NightAct;
                _state.Stage = StateStage.Night;
                break;
            
            case StateCommand.ShowLike:
                SleepPlayers(GetNextPlayerToSleep());
                break;
            
            case StateCommand.NightAct:
                SleepPlayers(GetNextPlayerToSleep());
                break;
            
            case StateCommand.Speak:
                break;
            case StateCommand.Vote:
                break;
            case StateCommand.GoodMorning:
                break;
            case StateCommand.Defending:
                break;
            case StateCommand.DefendingVote:
                break;
            case StateCommand.LastChangeCard:
                break;
            default:
                break;
            
        }
    }

    private void ReadyToNewMorning()
    {
        _state.Stage = StateStage.Morning;
        _state.Command = StateCommand.Ready;
        _state.Audience = StateAudience.All;
        var newRound = new Round()
        {
            TurnNumber = CurrentRound.TurnNumber + 1,
            IsInterview = false,
            RoundPlayers = CurrentRound.RoundPlayers,
            IsCurrentRound = true,
            NightKills = CurrentRound.NightKills
        };
        CurrentRound.IsCurrentRound = false;
        _state.CurrentPlayers.Clear();
        GameRounds.Add(newRound);
        foreach (var gamePlayer in GamePlayers)
        {
            gamePlayer.IsAwake = true;
            gamePlayer.HasSpoken = false;
            gamePlayer.Card!.IsAWakedInNight = false;
        }
    }


    private void DoInterviewEveningLogic(ScenarioState state)
    {
        if (CurrentRound.IsInterview && state.Command == StateCommand.Ready)
        {
            _state.Stage = StateStage.Night;
            _state.Command = StateCommand.Ready;
            _state.Audience = StateAudience.All;
            _state.CurrentPlayers.Clear();
        }
    }

    private void DoInterviewDayLogic(ScenarioState state)
    {
        switch (state.Command)
        {
            case StateCommand.Speak when CurrentRound.IsInterview && !HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Day;
                _state.Command = StateCommand.Speak;
                _state.Audience = StateAudience.Player;
                SetCurrentPlayers(GetNextPlayerToTalk()!);
                break;
            case StateCommand.Speak when CurrentRound.IsInterview && HaveAllThePlayersSpoken():
                _state.Stage = StateStage.Evening;
                _state.Command = StateCommand.Ready;
                _state.Audience = StateAudience.All;
                _state.CurrentPlayers.Clear();
                break;
            default:
                break;
        }
    }

    private void DoInterviewMorningLogic(ScenarioState state)
    {
        if (CurrentRound.IsInterview && state.Command == StateCommand.Ready)
        {
            _state.Stage = StateStage.Day;
            _state.Command = StateCommand.Speak;
            _state.Audience = StateAudience.Player;
            SetCurrentPlayers(GetNextPlayerToTalk()!);
        }
    }

    private void ShowLikePlayers(IEnumerable<Player> players)
    {
        if (players.All(x => x.Card.Side == CardSide.Mafia) && players.Count() > 1)
        {
            _state.Audience = StateAudience.MafiaGroup;
            _state.Command = StateCommand.ShowLike;
        }
        else if (GamePlayers.Count == players.Count())
        {
            _state.Audience = StateAudience.All;
            _state.Command = StateCommand.ShowLike;
        }
        else
        {
            _state.Audience = StateAudience.Player;
            _state.Command = StateCommand.ShowLike;
        }

        foreach (var player in players)
        {
            player.HasTheLikeShown = true;
        }

        SetCurrentPlayers(players);
    }

    private void WakeUpPlayers(IEnumerable<Player> players)
    {
        if (players.All(x => x.Card.Side == CardSide.Mafia) && players.Count() > 1)
        {
            _state.Audience = StateAudience.MafiaGroup;
            _state.Command = StateCommand.WakeUp;
        }
        else if (GamePlayers.Count == players.Count())
        {
            _state.Audience = StateAudience.All;
            _state.Command = StateCommand.WakeUp;
        }
        else
        {
            _state.Audience = StateAudience.Player;
            _state.Command = StateCommand.WakeUp;
        }

        foreach (var player in players)
        {
            player.IsAwake = true;
        }

        SetCurrentPlayers(players);
    }

    private void SleepPlayers(IEnumerable<Player> players)
    {
        if (players.All(x => x.Card.Side == CardSide.Mafia) && players.Count() > 1)
        {
            _state.Stage = StateStage.Night;
            _state.Audience = StateAudience.MafiaGroup;
            _state.Command = StateCommand.Sleep;
        }
        else if (GamePlayers.Count == players.Count())
        {
            _state.Stage = StateStage.Night;
            _state.Audience = StateAudience.All;
            _state.Command = StateCommand.Sleep;
        }
        else
        {
            _state.Stage = StateStage.Night;
            _state.Audience = StateAudience.Player;
            _state.Command = StateCommand.Sleep;
        }

        foreach (var player in players)
        {
            player.IsAwake = false;
        }

        SetCurrentPlayers(players);
    }

    private IEnumerable<Player> GetShowLikePlayers()
    {
        var unLikedPlayer = GamePlayers.OrderBy(x => x.Card.Order).First(x => x.IsAwake && !x.HasTheLikeShown);
        return new[] { unLikedPlayer };
    }

    private Player? GetNextPlayerToTalk()
    {
        var unTalkedPlayers = GamePlayers.Where(x => x.HasSpoken == false).OrderBy(x => x.TurnNumber);
        if (!_state.CurrentPlayers.Any())
        {
            var player = unTalkedPlayers.First();
            player.HasSpoken = true;
            return player;
        }
        else
        {
            var player = unTalkedPlayers.FirstOrDefault(x => x.TurnNumber > _state.CurrentPlayers.First()
                .TurnNumber);

            if (player != null)
            {
                player.HasSpoken = true;
            }

            return player;
        }
    }
    
    private Player? GetNextPlayerToVote()
    {
        var unvotedPlayers = CurrentRound.RoundPlayers.Where(x=>!x.HasVoteTaken);
        
        if (!_state.CurrentPlayers.Any())
        {
            var player = unvotedPlayers.First();
            player.HasVoteTaken = true;
            return player.Player;
        }
        else
        {
            var player = CurrentRound.RoundPlayers.Where(x=>!x.HasVoteTaken).FirstOrDefault(x => x.Player.TurnNumber > _state.CurrentPlayers.First()
                .TurnNumber);

            if (player != null)
            {
                player.HasVoteTaken = true;
            }

            return player.Player;
        }
    }

    private Player[] GetNextPlayerToWakeUp()
    {
        var unAWakedCard = GamePlayers.Where(x => !x.IsAwake).Select(x => x.Card)
            .Where(x => !x!.IsAWakedInNight && x.HasAbility).OrderBy(x => x?.Order);

        var card = unAWakedCard.First();

        var player = GamePlayers.First(x => x.Card?.GetType() == card?.GetType());
        card!.IsAWakedInNight = true;
        player.IsAwake = true;
        return new[] { player };
    }

    private Player[] GetNextPlayerToSleep()
    {
        var unAWakedCard = GamePlayers.OrderBy(x => x.Card.Order).Where(x => x.IsAwake);
        var player = unAWakedCard.First();
        return new[] { player };
    }

    private bool HaveAllThePlayersSpoken()
    {
        return GamePlayers.All(x => x.HasSpoken);
    }

    private void SetCurrentPlayers(IEnumerable<Player> players)
    {
        var copyPlayers = players.ToList();
        _state.CurrentPlayers.Clear();
        _state.CurrentPlayers.AddRange(copyPlayers);
    }

    private void SetCurrentPlayers(Player? player)
    {
        var copyPlayer = player;
        if (player == null)
        {
            _state.CurrentPlayers.Clear();
        }
        else
        {
            _state.CurrentPlayers.Clear();
            _state.CurrentPlayers.Add(copyPlayer);
        }
    }
}