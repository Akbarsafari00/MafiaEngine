namespace Mafia.Engine.Models.Cards;

public class LeonTheProfessionalCard : Card
{
    public LeonTheProfessionalCard(int order, bool wakeUpWithMafia = false) : base("لئون حرفه ای", CardSide.Citizen, true,order,wakeUpWithMafia)
    {
    }

    public override void Act(List<Player> Players)
    {
        throw new NotImplementedException();
    }
}