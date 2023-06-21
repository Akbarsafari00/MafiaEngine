namespace Mafia.Engine.Models.Cards;

public class CitizenKaneCard : Card
{
    public CitizenKaneCard(int order, bool wakeUpWithMafia = false) : base("همشهری کین", CardSide.Citizen, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}