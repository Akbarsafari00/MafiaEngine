namespace Mafia.Engine.Models.Cards;

public class CitizenCard : Card
{
    public CitizenCard(int order) : base("شهروند", CardSide.Citizen, false,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}