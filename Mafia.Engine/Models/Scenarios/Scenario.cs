using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public  interface IScenario {
    bool hasInterviewDay { get;  }
    List<Card> Cards { get;  }
    List<Step> InterViewTemplates { get; }
    List<Step> RoundTemplates { get; }
}