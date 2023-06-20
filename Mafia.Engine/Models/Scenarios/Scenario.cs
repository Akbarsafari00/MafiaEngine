using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public  interface IScenario {
    bool HasInterviewDay { get;  }
    List<Card> Cards { get;  }
    
}