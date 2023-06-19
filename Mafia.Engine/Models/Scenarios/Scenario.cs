using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public  interface IScenario {
    bool hasInterviewDay { get;  }
    List<Card> Cards { get;  }
    List<Activity> InterViewTemplates { get; }
}