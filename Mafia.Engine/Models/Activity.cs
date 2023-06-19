using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Activity 
{
    public Activity(ActivityAction action,ActivityType type, ActivityStage stage, Card? card = null, Player? player = null , int order = 0,bool isInterViewDay  = false)
    {
        Id = Guid.NewGuid();
        Type = type;
        Stage = stage;
        IsInterViewDay = isInterViewDay;
        Action = action;
        Player = player;
        Order = order;
    }

    public Guid Id { get; set; }
    public ActivityAction Action { get; set; }
    public ActivityType Type { get; set; }
    public ActivityStage Stage { get; set; }
    public bool IsInterViewDay { get; set; }
    public Player? Player { get; set; }
    public Card? Card { get; set; }
    public int Order { get; set; }
}


public enum ActivityType
{
    Wait,Speak,Vote,Message,
    WakeUp,
    Sleep,
    NightAct,
    SleepAllMafia,
    ShowLike,
    WakeUpAll,
    SleepAlLPlayer
}
public enum ActivityStage
{
    Morning,Day,Evening,Night
}

public enum ActivityAction
{
    ForSingle,ForEachPlayer
}