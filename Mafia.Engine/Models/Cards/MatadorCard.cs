namespace Mafia.Engine.Models.Cards;

public class MatadorCard : Card
{
    public MatadorCard(int order) : base("ماتادور", CardSide.Mafia, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}