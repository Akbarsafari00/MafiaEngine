using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class ScenarioState
{
    public Guid Id { get; set; }
    public GamePlayers CurrentPlayers { get; set; }
    public GameRounds GameRounds { get; set; }
    public StateStage Stage { get; set; }
    public StateCommand Command { get; set; }
    public StateAudience Audience { get; set; }
}

