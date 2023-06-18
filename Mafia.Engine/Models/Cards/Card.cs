namespace Mafia.Engine.Models.Cards;

public class Card
{
    public Card(string name, CardSide side,bool hasAbility, string description = "")
    {
        Name = name;
        Description = description;
        Side = side;
        HasAbility = hasAbility;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public CardSide Side { get; set; }
    public bool HasAbility { get; set; }
}

public enum CardSide
{
    Independent,Mafia,Citizen,
}


