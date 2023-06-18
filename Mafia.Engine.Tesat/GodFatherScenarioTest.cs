using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Test;

public class GodFatherScenarioTest
{
    
    private  GameEngine engine { get; set; }

    [SetUp]
    public void Setup()
    {
  
        
        engine = new GodFatherGameEngine(state);
        engine.AddPlayer(new Users("Akbar Ahmadi"));
        engine.AddPlayer(new Users("Hadi Ahmadi"));
        engine.AddPlayer(new Users("Ali Ahmadi"));
        engine.AddPlayer(new Users("Mojtaba Ali Mohamadi"));
        engine.AddPlayer(new Users("Leila Sadegi"));
        engine.AddPlayer(new Users("Fatemeh Ahmadi"));
        engine.AddPlayer(new Users("Zahra Ahmadi"));
        engine.AddPlayer(new Users("Nosrat Ahmadi"));
        engine.AddPlayer(new Users("AmirHosein Yagooti"));
        engine.AddPlayer(new Users("Raziyeh Abbasi"));
        engine.AddPlayer(new Users("Saedeh Abbasi"));
    }

    [Test]
    public void ShuffleRolesTest()
    {
        engine.ShuffleCards();
        while (true)
        {
            Console.ReadLine();
            var state = engine.Execute();
        }
    }
}