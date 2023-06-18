using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models;

public class RoundPlayer
{
    public Player Player { get; set; }
    public bool IsTalked { get; set; } = false;
    public int VoteCount { get; set; } = 0;
}