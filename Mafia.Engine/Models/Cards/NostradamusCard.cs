namespace Mafia.Engine.Models.Cards;

public class NostradamusCard : Card
{
    public NostradamusCard(int order, bool wakeUpWithMafia = false) : base("نوستراداموس", CardSide.Independent, true,order , wakeUpWithMafia,true)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}