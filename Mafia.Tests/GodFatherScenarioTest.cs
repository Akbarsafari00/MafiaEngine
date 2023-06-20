using Mafia.Engine;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;
using Mafia.Engine.Models.Scenarios;

namespace Mafia.Tests;

public class GodFatherScenarioTest
{
    private ScenarioEngine _engine;
    private ScenarioState _state;
    
    [SetUp]
    public void Setup()
    {
        _engine = new ScenarioEngine(new GodFatherScenario());
        _engine.AddPlayer(new Users("Akbar Ahmadi"));
        _engine.AddPlayer(new Users("Hadi Ahmadi"));
        _engine.AddPlayer(new Users("Ali Ahmadi"));
        _engine.AddPlayer(new Users("Mojtaba Ali Mohamadi"));
        _engine.AddPlayer(new Users("Leila Sadegi"));
        _engine.AddPlayer(new Users("Fatemeh Ahmadi"));
        _engine.AddPlayer(new Users("Zahra Ahmadi"));
        _engine.AddPlayer(new Users("Nosrat Ahmadi"));
        _engine.AddPlayer(new Users("AmirHosein Yagooti"));
        _engine.AddPlayer(new Users("Raziyeh Abbasi"));
        _engine.AddPlayer(new Users("Saedeh Abbasi"));
        
        _state = _engine.ShuffleCards();
    }

    [Test]
    public void TestShuffleCard()
    {
        Assert.That(_engine.Players.Any(x => x.Card == null), Is.False);
    }
    
    [Test]
    public void TestInterviewScenario( )
    {
        Assert.Multiple(() =>
        {
            Assert.That(_state.Stage, Is.EqualTo(StateStage.Morning));
            Assert.That(_state.Audience, Is.EqualTo(StateAudience.All));
            Assert.That(_state.Command, Is.EqualTo(StateCommand.Ready));
            Assert.That(_state.CurrentPlayer, Is.EqualTo(null));
        });

        foreach (var player in _engine.Players.OrderBy(x=>x.TurnNumber))
        {
            _state = _engine.Execute(_state);
        
            Assert.Multiple(() =>
            {
                Assert.That(_state.Stage, Is.EqualTo(StateStage.Day));
                Assert.That(_state.Audience, Is.EqualTo(StateAudience.Player));
                Assert.That(_state.Command, Is.EqualTo(StateCommand.Speak));
                Assert.That(_state.CurrentPlayer?.TurnNumber, Is.EqualTo(player.TurnNumber));
            });
        }
        
        _state = _engine.Execute(_state);
        
        Assert.Multiple(() =>
        {
            Assert.That(_state.Stage, Is.EqualTo(StateStage.Evening));
            Assert.That(_state.Audience, Is.EqualTo(StateAudience.All));
            Assert.That(_state.Command, Is.EqualTo(StateCommand.Ready));
            Assert.That(_state.CurrentPlayer?.TurnNumber, Is.EqualTo(null));
        });
        
        _state = _engine.Execute(_state);
        
        Assert.Multiple(() =>
        {
            Assert.That(_state.Stage, Is.EqualTo(StateStage.Night));
            Assert.That(_state.Audience, Is.EqualTo(StateAudience.All));
            Assert.That(_state.Command, Is.EqualTo(StateCommand.Ready));
            Assert.That(_state.CurrentPlayer?.TurnNumber, Is.EqualTo(null));
        });
        
        _state = _engine.Execute(_state);
        
        Assert.Multiple(() =>
        {
            Assert.That(_state.Stage, Is.EqualTo(StateStage.Night));
            Assert.That(_state.Audience, Is.EqualTo(StateAudience.Player));
            Assert.That(_state.Command, Is.EqualTo(StateCommand.WakeUp));
            Assert.That(_state.CurrentPlayer?.TurnNumber, Is.Not.EqualTo(null));
            Assert.That(_state.CurrentPlayer?.Card?.GetType(), Is.EqualTo(typeof(NostradamusCard)));
        });
        
    }
    
}