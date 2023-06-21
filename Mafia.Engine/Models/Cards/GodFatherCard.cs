namespace Mafia.Engine.Models.Cards;

public class GodFatherCard : Card
{
    public GodFatherCard(int order , bool wakeUpWithMafia = true) : base("پدرخوانده", CardSide.Mafia, true,order , wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}