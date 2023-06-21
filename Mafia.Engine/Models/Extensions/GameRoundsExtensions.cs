namespace Mafia.Engine.Models.Extensions;

public static class GameRoundsExtensions
{
    public static Round CurrentRound(this GameRounds rounds)
        => rounds.First(x => x.IsCurrentRound);
    
}