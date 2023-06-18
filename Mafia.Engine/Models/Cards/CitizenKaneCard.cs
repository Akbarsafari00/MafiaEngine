namespace Mafia.Engine.Models.Cards;

public class CitizenKaneCard : Card
{
    public CitizenKaneCard(int order) : base("همشهری کین", CardSide.Citizen, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}