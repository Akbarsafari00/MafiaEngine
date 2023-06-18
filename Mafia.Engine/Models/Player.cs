using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Player
{
    public Guid Id { get; set; }
    public bool IsDead { get; set; } = false ;
    public Users User { get; set; }
    public Card? Card { get; set; }
    public int TurnNumber { get; set; }
    public bool IsWakeUp { get; set; } = true;
}