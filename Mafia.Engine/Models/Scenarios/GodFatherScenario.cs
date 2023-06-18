using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public class GodFatherScenario
{
    public List<Activity> InterViewTemplates = new List<Activity>()
    {
        new Activity(ActivityAction.ForSingle,ActivityType.Wait,ActivityStage.Day),
        new Activity(ActivityAction.ForEachPlayer,ActivityType.Speak,ActivityStage.Day),
        
        new Activity(ActivityAction.ForSingle,ActivityType.Vote,ActivityStage.Evening),
        
        new Activity(ActivityAction.ForSingle,ActivityType.SleepAlLPlayer,ActivityStage.Night),
        
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night ,new NostradamusCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.NightAct,ActivityStage.Night,new NostradamusCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.Sleep,ActivityStage.Night,new NostradamusCard(0)),
        
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night,new GodFatherCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night,new MatadorCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night,new SaulGoodmanCard(0)),
        
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new GodFatherCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new MatadorCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new SaulGoodmanCard(0)),
        
        new Activity(ActivityAction.ForSingle,ActivityType.SleepAllMafia,ActivityStage.Night),
        
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night ,new CitizenKaneCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new CitizenKaneCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.Sleep,ActivityStage.Night,new CitizenKaneCard(0)),
       
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night ,new DrWatsonCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new DrWatsonCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.Sleep,ActivityStage.Night,new DrWatsonCard(0)),
       
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night ,new LeonTheProfessionalCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new LeonTheProfessionalCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.Sleep,ActivityStage.Night,new LeonTheProfessionalCard(0)),
       
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUp,ActivityStage.Night ,new ConstantineCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.ShowLike,ActivityStage.Night,new ConstantineCard(0)),
        new Activity(ActivityAction.ForSingle,ActivityType.Sleep,ActivityStage.Night,new ConstantineCard(0)),
       
        new Activity(ActivityAction.ForSingle,ActivityType.WakeUpAll,ActivityStage.Night),
        
    };
}