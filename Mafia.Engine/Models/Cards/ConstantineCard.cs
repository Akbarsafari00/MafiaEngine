namespace Mafia.Engine.Models.Cards;

public class ConstantineCard : Card
{
    public ConstantineCard(int order) : base("کنستانتین", CardSide.Citizen, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}

