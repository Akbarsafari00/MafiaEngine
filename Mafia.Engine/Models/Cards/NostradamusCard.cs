namespace Mafia.Engine.Models.Cards;

public class NostradamusCard : Card
{
    public NostradamusCard(int order) : base("نوستراداموس", CardSide.Independent, true,order , true)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}