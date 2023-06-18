using Mafia.Engine.Models;

namespace Mafia.Engine;

public class GameState
{
    public Guid Id { get; set; }
    public bool hasInterviewDay { get; set; }
    public List<Player> Players { get; set; }
    public List<Round> Rounds { get; set; }
    public Round? CurrentRound => Rounds.FirstOrDefault(x => x.TurnNumber == CurrentDay);
    public int CurrentDay { get; set; }
    public GameAction Action { get; set; }
    public Player? CurrentPlayer { get; set; }
}

public enum GameStage
{
    Morning,Day,Evening ,Night
}

public enum GameAction
{
    Pending,Talking,Voting,Acting,Finished
}