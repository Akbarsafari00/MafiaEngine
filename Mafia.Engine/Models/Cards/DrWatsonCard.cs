namespace Mafia.Engine.Models.Cards;

public class DrWatsonCard : Card
{
    public DrWatsonCard(int order) : base("دکتر واتسون", CardSide.Citizen, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}