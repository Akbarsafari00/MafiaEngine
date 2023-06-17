namespace Mafia.Engine.Models;

public class Users
{
    public Users(string name, string number = "")
    {
        Name = name;
        Number = number;
    }

    public string Name { get; set; }
    public string Number { get; set; }
}