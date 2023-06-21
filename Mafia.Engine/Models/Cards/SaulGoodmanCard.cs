namespace Mafia.Engine.Models.Cards;

public class SaulGoodmanCard : Card
{
    public SaulGoodmanCard(int order, bool wakeUpWithMafia = true) : base("سائول گودمن", CardSide.Mafia, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}