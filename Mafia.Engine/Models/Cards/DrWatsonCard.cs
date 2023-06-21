namespace Mafia.Engine.Models.Cards;

public class DrWatsonCard : Card
{
    public DrWatsonCard(int order, bool wakeUpWithMafia = false) : base("دکتر واتسون", CardSide.Citizen, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}