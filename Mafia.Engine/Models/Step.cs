using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Step 
{
    public Step(ActivityAction action,Action<GameState, Player>? systemAction)
    {
        Action = action;
        SystemAction = systemAction;
    }

    public ActivityAction Action { get; set; }
    public Action<GameState,Player>? SystemAction { get; set; }
}


public enum StepType
{
    Note,
    Speak,
    Vote,
    WakeUp,
    Sleep,
    NightAct,
    ShowLike,
    GoodMorning,
    CheckVote,
    Defending,
    DefendingVote,
    CheckDefendingVote,
    LastChangeCard,
    InterviewSpeak
}
public enum StepStage
{
    Morning,Day,Evening,Night
}

public enum ActivityAction
{
    System,None,LoopPlayer,PlayerFromCard, LoopDefending
}


public enum StepAudience
{
    All,Citizen,CitizenGroup,Mafia,MafiaGroup,CitizenHaveAct,Dead
}