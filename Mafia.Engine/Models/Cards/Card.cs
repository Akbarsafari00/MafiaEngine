namespace Mafia.Engine.Models.Cards;

public abstract class Card
{
    public Card(string name, CardSide side,bool hasAbility,int order, bool wakeUpWithMafia = false, bool interViewAct = false, string description = "")
    {
        Name = name;
        Description = description;
        Side = side;
        HasAbility = hasAbility;
        Order = order;
        WakeUpWithMafia = wakeUpWithMafia;
        HasInterViewAct = interViewAct;
    }

    public string Name { get; set; }
    public int Order { get; set; }
    public bool WakeUpWithMafia { get; set; } = false;
    public bool HasInterViewAct { get; set; } = false;
    public bool IsAWakedInNight { get; set; } = false;
    public string Description { get; set; }
    public CardSide Side { get; set; }
    public bool HasAbility { get; set; }


    public abstract void Act(List<Player> Players);
}

public enum CardSide
{
    Independent,Mafia,Citizen,
}


