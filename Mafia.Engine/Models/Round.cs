namespace Mafia.Engine.Models;

public class Round
{
    public GameStage Stage { get; set; }
    
    public int TurnNumber { get; set; }
    
    public List<RoundPlayer> RoundPlayers { get; set; } = new();
    public List<Player> NightKills { get; set; } = new();
    public GameStage NextStage { get; set; }
}