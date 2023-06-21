namespace Mafia.Engine.Models.Cards;

public class ConstantineCard : Card
{
    public ConstantineCard(int order, bool wakeUpWithMafia = false) : base("کنستانتین", CardSide.Citizen, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}

