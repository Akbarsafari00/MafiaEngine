using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Step
{
    public Step(StepType type,StateStage stage,StateAudience audience , StateCommand command,
        Action<ScenarioState, Player>? stateAction = null,
        Player? player = null,
        Card? card = null)
    {
        Type = type;
        StateAction = stateAction;
        Player = player;
        Card = card;
        Stage = stage;
        Command = command;
        Audience = audience;
    }

    public StepType Type { get; private set; }
    public StateStage Stage { get; private set; }
    public StateCommand Command { get; private set; }
    public StateAudience Audience { get; private set; }
    public Player? Player { get; private set; }
    public Card? Card { get; private set; }
    public Action<ScenarioState, Player>? StateAction { get; private set; }
    public bool IsInterviewDay { get; set; }
}