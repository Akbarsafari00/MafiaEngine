using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public class GodFatherScenario : IScenario
{
    public bool HasInterviewDay => true;

    public List<Card> Cards => new()
    {
        new NostradamusCard(1),
        new GodFatherCard(2),
        new MatadorCard(3),
        new SaulGoodmanCard(4),
        new DrWatsonCard(5),
        new LeonTheProfessionalCard(6),
        new CitizenKaneCard(7),
        new ConstantineCard(8),
        new CitizenCard(9),
    };
}