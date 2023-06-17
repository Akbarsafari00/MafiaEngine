using Mafia.Engine.Extensions;
using Mafia.Engine.Models;

namespace Mafia.Engine;

public class MafiaEngine
{
    public MafiaInstance Instance { get; set; }
    public Player? ActivePlayer { get; set; }
    public  List<Player> Players { get; set; } 
    public MafiaEngine(MafiaInstance instance)
    {
        Instance = instance;
        Players = new List<Player>();
    }

    public void ShuffleRoles ()
    {
        Instance.CheckUserCount();
        var index = 1;

        var users = Instance.Users;

        foreach (var role in Instance.Roles.Where(x=>x.Side == RoleSide.Mafia))
        {
            
            var user = users.PickRandom();
            Players.Add(new Player()
            {
                User = user,
                Role = role,
                Index = index
            });
            users.Remove(user);
            index++;
        }
        
        foreach (var role in Instance.Roles.Where(x=>x is { Side: RoleSide.Citizen, HasAbility: true }))
        {
            var user = users.PickRandom();
            Players.Add(new Player()
            {
                User = user,
                Role = role,
                Index = index
            });
            users.Remove(user);
            index++;
        }
        
        foreach (var role in Instance.Roles.Where(x=>x is { Side: RoleSide.Citizen, HasAbility: false }))
        {
            foreach (var user in users)
            {
                Players.Add(new Player()
                {
                    User = user,
                    Role = role,
                    Index = index
                });
                index++;
            }
        }
    }    
    public void Execute()
    {

        if (Players.Count <0)
        {
            throw new Exception("Not Found Player With Role");
        }
        
        while (Instance.State is not (MafiaState.Finished and MafiaState.Waiting))
        {
            if (Instance.DayState==MafiaDayState.Day)
            {
                foreach (var player in Players)
                {
                    Instance.State = MafiaState.Spoken;
                    ActivePlayer = player;
                }
            }
        }
    }
}

public class Player
{
    public Users User { get; set; }
    public Roles Role { get; set; }
    public int Index { get; set; }
}