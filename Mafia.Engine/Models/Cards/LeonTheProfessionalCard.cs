namespace Mafia.Engine.Models.Cards;

public class LeonTheProfessionalCard : Card
{
    public LeonTheProfessionalCard(int order) : base("لئون حرفه ای", CardSide.Citizen, true,order)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}