using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class Player
{
    public Users User { get; set; }
    public Card? Card { get; set; }
    public bool HaveChallenge { get; set; } = false;
    public int Index { get; set; }
}