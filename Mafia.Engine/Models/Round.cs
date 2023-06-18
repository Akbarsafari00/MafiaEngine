namespace Mafia.Engine.Models;

public class Round
{
    public int Number { get; set; }
    public List<PlayerVotes> Votes { get; set; }
    public List<Player> NightKills { get; set; }
}