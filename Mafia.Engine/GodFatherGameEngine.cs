using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class GodFatherGameEngine : GameEngine
{
    public GodFatherGameEngine() : base(new List<Card>()
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
    }, 11, true)
    {
    }
}