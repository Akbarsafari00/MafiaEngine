namespace Mafia.Engine.Models;

public class Roles
{
    public Roles(string name, RoleSide side,bool hasAbility , string description = "")
    {
        Name = name;
        Description = description;
        Side = side;
        HasAbility = hasAbility;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public RoleSide Side { get; set; }
    public bool HasAbility { get; set; }
}

public enum RoleSide
{
    None,Mafia,Citizen,
    
}

