using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class GameState
{
    public Guid Id { get; set; }
    public List<Player> Players { get; set; }
    public List<Round> Rounds { get; set; }
    public Round CurrentRound => Rounds.First(x => x.IsCurrentRound);
    public StepStage Stage { get; set; }
    public StepType Type { get; set; }
    public string Message { get; set; }
}

