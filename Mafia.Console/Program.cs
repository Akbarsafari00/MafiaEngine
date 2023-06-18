using Mafia.Engine;
using Mafia.Engine.Models;

Console.WriteLine("Please Choose Scenario To Start.");
Console.ReadLine();

var engine = new GodFatherGameEngine();
engine.AddPlayer(new Users("Akbar Ahmadi"));
engine.AddPlayer(new Users("Hadi Ahmadi"));
engine.AddPlayer(new Users("Ali Ahmadi"));
engine.AddPlayer(new Users("Mojtaba Ali Mohamadi"));
engine.AddPlayer(new Users("Leila Sadegi"));
engine.AddPlayer(new Users("Fatemeh Ahmadi"));
engine.AddPlayer(new Users("Zahra Ahmadi"));
engine.AddPlayer(new Users("Nosrat Ahmadi"));
engine.AddPlayer(new Users("AmirHosein Yagooti"));
engine.AddPlayer(new Users("Raziyeh Abbasi"));
engine.AddPlayer(new Users("Saedeh Abbasi"));

Console.WriteLine("Press Enter To Shuffle Cards . ");
Console.ReadLine();

engine.ShuffleCards();

foreach (var player in engine.Players())
{
    Console.WriteLine($"- {player.User.Name} [{player.Card?.GetType().Name}]");
}


var state = engine.GetState();

// Day - Pending

while (true)
{
    if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Pending && state.CurrentDay == 0)
    {
            Console.WriteLine("\n Message : Bazikonan Shoro Be Sohbat Baraye Moarefeh Mikonn");
            Console.WriteLine("\n ** Shoro **");
            Console.ReadLine();
            state = engine.Execute();
            
    }
    else if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Talking && state.CurrentDay == 0)
    {
        Console.WriteLine($"\n Message : Bazikon [{state.CurrentPlayer.TurnNumber}][{state.CurrentPlayer.User.Name}] Dar Hal Moarefe Khod Ast");
        Console.WriteLine("\n ** Nafar Badi **");
        Console.ReadLine();
        state = engine.Execute();
            
    }
    else if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Finished && state.CurrentDay == 0)
    {
        Console.WriteLine($"\n Message : Bazikon [{state.CurrentPlayer.TurnNumber}][{state.CurrentPlayer.User.Name}] Dar Hal Moarefe Khod Ast");
        Console.WriteLine("\n ** Etmam Goftego **");
        Console.ReadLine();
        state = engine.Execute();
            
    }
    else if (state.CurrentRound?.Stage == GameStage.Evening && state.Action == GameAction.Finished && state.CurrentDay == 0)
    {
        Console.WriteLine($"\n Message : Dar Roz Moarefe Ray giri nadarim v b shab moarefe miravim");
        Console.WriteLine("\n ** Berim Shab **");
        Console.ReadLine();
        state = engine.Execute();
            
    }
    else if (state.CurrentRound.NextStage == GameStage.Evening)
    {
        if (state.Action == GameAction.Pending)
        {
            Console.WriteLine("\n Press Enter To Start Voting . ");
            Console.ReadLine();
            state = engine.Execute();
        }
        else
        {
            Console.WriteLine($"\n Press Enter Vote For: [{state.CurrentPlayer?.User.Name}]");
            var vote=  int.Parse((string.IsNullOrWhiteSpace(Console.ReadLine())?"0":Console.ReadLine()) ?? string.Empty);
            if (state.CurrentPlayer != null) engine.Vote(state.CurrentPlayer, vote);
            state = engine.Execute();
        }
        
    }
    
    PrintInformation();
   
}

 void PrintInformation()
{
    // Console.WriteLine($"TurnNumber : {state.CurrentPlayer?.TurnNumber} ");
    // Console.WriteLine($"Player : {state.CurrentPlayer?.User.Name} ");
    // Console.WriteLine($"Stage : {state.CurrentRound.Stage} ");
    // Console.WriteLine($"Action : {state.Action} ");
}
