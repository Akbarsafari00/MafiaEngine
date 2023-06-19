namespace Mafia.Engine.Models;

public class Round
{
    public int TurnNumber { get; set; }
    public bool IsCurrentRound { get; set; }
    public List<RoundPlayer> RoundPlayers { get; set; } = new();
    public List<Player> NightKills { get; set; } = new();
    public bool IsInterview { get; set; }
}