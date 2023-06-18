using Mafia.Engine.Models;

namespace Mafia.Engine;

public class GameState
{
    public Guid Id { get; set; }
    public bool hasInterviewDay { get; set; }
    public List<Round> Rounds { get; set; }
    public Round? CurrentRound => Rounds.FirstOrDefault(x => x.Number == CurrentDay);
    public int CurrentDay { get; set; } 
    public GameStage Stage { get; set; }
    public GameAction Action { get; set; }
    public Player? CurrentPlayer { get; set; }
}

public enum GameStage
{
    Night,Day,Voting 
}

public enum GameAction
{
    NotStarted,Talking,Voting,Acting
}