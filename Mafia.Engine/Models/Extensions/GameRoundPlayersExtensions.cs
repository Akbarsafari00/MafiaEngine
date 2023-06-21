using System.Linq.Expressions;

namespace Mafia.Engine.Models.Extensions;

public static class GameRoundPlayersExtensions
{
    public static IOrderedEnumerable<RoundPlayer> OrderByCard(this GameRoundPlayers roundPlayers) =>
        roundPlayers.OrderBy(x => x.Player.Card.Order);
    public static IOrderedEnumerable<RoundPlayer> OrderByPlayerTurn(this GameRoundPlayers roundPlayers) =>
        roundPlayers.OrderBy(x => x.Player.TurnNumber);

    public static RoundPlayer First(this GameRoundPlayers roundPlayers) => roundPlayers.First();
    public static RoundPlayer Last(this GameRoundPlayers roundPlayers) => roundPlayers.OrderByPlayerTurn().Last();
    
    public static bool IsFirst(this GameRoundPlayers roundPlayers, RoundPlayer player) => roundPlayers.First().Player.Id.Equals(player.Player.Id);
    public static bool IsLast(this GameRoundPlayers roundPlayers, RoundPlayer player) => roundPlayers.Last().Player.Id.Equals(player.Player.Id);
    public static bool IsAllSleep(this GameRoundPlayers roundPlayers) => roundPlayers.All(x => !x.Player.IsAwake);
    public static bool IsAllAwake(this GameRoundPlayers roundPlayers) => roundPlayers.All(x => x.Player.IsAwake);
    
    public static bool IsAnyPlayerWakeUpWithMafia(this GameRoundPlayers roundPlayers,
        Func<RoundPlayer, bool>? filter = null)
        => filter == null
            ? roundPlayers.Any(x => x.Player.Card.WakeUpWithMafia)
            : roundPlayers.Any(x => x.Player.Card.WakeUpWithMafia && filter.Invoke(x));
    
    public static bool IsAllAbilityPlayersAWakedUpInNight(this GameRoundPlayers roundPlayers) => 
        roundPlayers.Where(x=>x.Player.Card.HasAbility).All(x => x.Player.Card.IsAWakedInNight);
}