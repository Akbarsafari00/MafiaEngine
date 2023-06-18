namespace Mafia.Engine.Models.Cards;

public class SaulGoodmanCard : Card
{
    public SaulGoodmanCard(int order) : base("سائول گودمن", CardSide.Mafia, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}