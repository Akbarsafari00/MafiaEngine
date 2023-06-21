using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class Player
{
    public Guid Id { get; set; }

    public Users User { get; set; }
    public Card Card { get; set; }
    public int TurnNumber { get; set; }
    public bool HasSpoken { get; set; } = false;
    public bool IsAwake { get; set; } = true;
    public bool HasTheLikeShown { get; set; } = false;
    public bool IsDead { get; set; } = false;
}