using Mafia.Engine.Models;
using Mafia.Engine.Models.Cards;

namespace Mafia.Engine;

public class MafiaInstance
{
    public MafiaInstance(int maxPlayer, int minPlayer, bool haveInterviewDay = true)
    {
        DayState = MafiaDayState.Day;
        State = MafiaState.Pending;
        MaxPlayer = maxPlayer;
        MinPlayer = minPlayer;
        HaveInterviewDay = haveInterviewDay;
        CurrentDay = haveInterviewDay ? 0 : 1;
    }

    public MafiaDayState DayState { get; set; }
    public MafiaState State { get; set; }
    public Player ActivePlayer { get; set; }

    public int MaxPlayer { get; set; }
    public int MinPlayer { get; set; }
    public int CurrentDay { get; set; }
    public bool HaveInterviewDay { get; set; }

    public List<Card> Roles = new();
    public List<Users> Users = new();

    public  List<Player> Players = new();
    



    public void CheckUserCount()
    {
        if (Users.Count<MinPlayer)
        {
            throw new Exception("User less then MinPlayer");
        }
        
        if (Users.Count>MaxPlayer)
        {
            throw new Exception("User more then MaxPlayer");
        }
    }

}

public enum MafiaDayState
{
    Day,Night
}

public enum MafiaState
{
    Pending,Finished,Waiting,
    Spoken
}