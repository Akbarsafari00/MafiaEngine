using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class GodFatherGameEngine : GameEngine
{
    public GodFatherGameEngine() : base(new List<Card>()
    {
        new GodFatherCard(),
        new CitizenCard(),
        new ConstantineCard(),
        new MatadorCard(),
        new NostradamusCard(),
        new CitizenKaneCard(),
        new DrWatsonCard(),
        new SaulGoodmanCard(),
        new LeonTheProfessionalCard()
    }, 11, true)
    {
    }
}