namespace Mafia.Engine.Models.Cards;

public class MatadorCard : Card
{
    public MatadorCard(int order, bool wakeUpWithMafia = true) : base("ماتادور", CardSide.Mafia, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}