using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public class GodFatherScenario : IScenario
{
    public bool hasInterviewDay  => true;

    public List<Card> Cards => new()
    {
        new GodFatherCard(2),
        new CitizenCard(9),
        new ConstantineCard(8),
        new MatadorCard(3),
        new NostradamusCard(1),
        new CitizenKaneCard(7),
        new DrWatsonCard(5),
        new SaulGoodmanCard(4),
        new LeonTheProfessionalCard(6)
    };

    public List<Activity> InterViewTemplates =>  new()
    {
        new Activity(ActivityAction.ForSingle, ActivityType.Wait, ActivityStage.Day , isInterViewDay:true),
        new Activity(ActivityAction.ForEachPlayer, ActivityType.Speak, ActivityStage.Day, isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.Vote, ActivityStage.Evening, isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.SleepAlLPlayer, ActivityStage.Night, isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new NostradamusCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.NightAct, ActivityStage.Night, new NostradamusCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.Sleep, ActivityStage.Night, new NostradamusCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new GodFatherCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new MatadorCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new SaulGoodmanCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new GodFatherCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new MatadorCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new SaulGoodmanCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.SleepAllMafia, ActivityStage.Night, isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new CitizenKaneCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new CitizenKaneCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.Sleep, ActivityStage.Night, new CitizenKaneCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new DrWatsonCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new DrWatsonCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.Sleep, ActivityStage.Night, new DrWatsonCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night,
            new LeonTheProfessionalCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night,
            new LeonTheProfessionalCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.Sleep, ActivityStage.Night, new LeonTheProfessionalCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUp, ActivityStage.Night, new ConstantineCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.ShowLike, ActivityStage.Night, new ConstantineCard(0), isInterViewDay:true),
        new Activity(ActivityAction.ForSingle, ActivityType.Sleep, ActivityStage.Night, new ConstantineCard(0), isInterViewDay:true),

        new Activity(ActivityAction.ForSingle, ActivityType.WakeUpAll, ActivityStage.Night, isInterViewDay:true),

    };



}