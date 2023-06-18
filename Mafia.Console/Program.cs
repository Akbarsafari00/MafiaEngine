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
while (true)
{
    if (state.CurrentRound.NextStage == GameStage.Day)
    {
        if (state.Action == GameAction.Pending)
        {
            Console.WriteLine("\n Press Enter To Start Talking . ");
            Console.ReadLine();
            state = engine.Execute();
            PrintInformation();
        }
        else
        {
            Console.WriteLine($"\n Press Enter To Start Talk : [{state.CurrentPlayer?.User.Name}] ");
            Console.ReadLine();
            state = engine.Execute();
            PrintInformation();
        }
    }
    else if (state.CurrentRound.NextStage == GameStage.Evening)
    {
        if (state.Action == GameAction.Pending)
        {
            Console.WriteLine("\n Press Enter To Start Voting . ");
            Console.ReadLine();
            state = engine.Execute();
            PrintInformation();
        }
        else
        {
            Console.WriteLine($"\n Press Enter Vote For: [{state.CurrentPlayer?.User.Name}]");
            var vote=  int.Parse((string.IsNullOrWhiteSpace(Console.ReadLine())?"0":Console.ReadLine()) ?? string.Empty);
            if (state.CurrentPlayer != null) engine.Vote(state.CurrentPlayer, vote);
            state = engine.Execute();

            PrintInformation();
        }
        
    }
    
   
}

 void PrintInformation()
{
    Console.WriteLine($"TurnNumber : {state.CurrentPlayer?.TurnNumber} ");
    Console.WriteLine($"Player : {state.CurrentPlayer?.User.Name} ");
    Console.WriteLine($"Stage : {state.CurrentRound.NextStage} ");
    Console.WriteLine($"Action : {state.Action} ");
}
