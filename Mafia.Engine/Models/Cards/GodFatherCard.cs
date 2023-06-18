namespace Mafia.Engine.Models.Cards;

public class GodFatherCard : Card
{
    public GodFatherCard(int order) : base("پدرخوانده", CardSide.Mafia, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}