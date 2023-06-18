using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Activity 
{
    public Activity(ActivityAction action,ActivityType type, ActivityStage activityStage,  Card? card = null, Player? player = null )
    {
        Id = Guid.NewGuid();
        Type = type;
        ActivityStage = activityStage;
        Action = action;
        Player = player;
    }

    public Guid Id { get; set; }
    public ActivityAction Action { get; set; }
    public ActivityType Type { get; set; }
    public ActivityStage ActivityStage { get; set; }
    public Player? Player { get; set; }
    public Card? Card { get; set; }
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