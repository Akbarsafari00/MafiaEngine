namespace Mafia.Engine.Models;

public class Round
{
    public GameStage Stage { get; set; }
    
    public int TurnNumber { get; set; }
    
    public List<RoundPlayer> RoundPlayers { get; set; } = new List<RoundPlayer>();
    public List<Player> NightKills { get; set; } = new List<Player>();
    public GameStage NextStage { get; set; }
}