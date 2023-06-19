// using Mafia.Engine;
// using Mafia.Engine.Models;
// using Mafia.Engine.Models.Cards;
//
// Console.WriteLine("Please Choose Scenario To Start.");
// Console.ReadLine();
//
// var engine = new GodFatherGameEngine();
// engine.AddPlayer(new Users("Akbar Ahmadi"));
// engine.AddPlayer(new Users("Hadi Ahmadi"));
// engine.AddPlayer(new Users("Ali Ahmadi"));
// engine.AddPlayer(new Users("Mojtaba Ali Mohamadi"));
// engine.AddPlayer(new Users("Leila Sadegi"));
// engine.AddPlayer(new Users("Fatemeh Ahmadi"));
// engine.AddPlayer(new Users("Zahra Ahmadi"));
// engine.AddPlayer(new Users("Nosrat Ahmadi"));
// engine.AddPlayer(new Users("AmirHosein Yagooti"));
// engine.AddPlayer(new Users("Raziyeh Abbasi"));
// engine.AddPlayer(new Users("Saedeh Abbasi"));
//
// Console.WriteLine("Press Enter To Shuffle Cards . ");
// Console.ReadLine();
//
// engine.ShuffleCards();
//
// foreach (var player in engine.Players())
// {
//     Console.WriteLine($"- {player.User.Name} [{player.Card?.GetType().Name}]");
// }
//
//
// var state = engine.GetState();
//
// // Day - Pending
//
// while (true)
// {
//     if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Pending && state.CurrentDay == 0)
//     {
//             Console.WriteLine("\n Message : Bazikonan Shoro Be Sohbat Baraye Moarefeh Mikonn");
//             Console.WriteLine("\n ** Shoro **");
//             Console.ReadLine();
//             state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Talking && state.CurrentDay == 0)
//     {
//         Console.WriteLine($"\n Message : Bazikon [{state.CurrentPlayer.TurnNumber}][{state.CurrentPlayer.User.Name}] Dar Hal Moarefe Khod Ast");
//         Console.WriteLine("\n ** Nafar Badi **");
//         Console.ReadLine();
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Day && state.Action == GameAction.Finished && state.CurrentDay == 0)
//     {
//         Console.WriteLine($"\n Message : Bazikon [{state.CurrentPlayer.TurnNumber}][{state.CurrentPlayer.User.Name}] Dar Hal Moarefe Khod Ast");
//         Console.WriteLine("\n ** Etmam Goftego **");
//         Console.ReadLine();
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Evening && state.Action == GameAction.Finished && state.CurrentDay == 0)
//     {
//         Console.WriteLine($"\n Message : Dar Roz Moarefe Ray giri nadarim v b shab moarefe miravim");
//         Console.WriteLine("\n ** Berim Shab **");
//         Console.ReadLine();
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Night && state.Action == GameAction.WakeUp && state.CurrentDay == 0)
//     {
//         Console.WriteLine($"\n {state.CurrentCard.GetType().Name} Bidar Beshavad.");
//         Console.WriteLine("\n ** Bidar Shod **");
//         Console.ReadLine();
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Night && state.Action == GameAction.Acting && state.CurrentDay == 0)
//     {
//         Console.WriteLine($"\n {state.CurrentCard.GetType().Name} Act Khod Ra Anjam Dahad.");
//         Console.WriteLine($"\n Shomare Nafarat Khod Ra Elam Knid");
//         var res = Console.ReadLine();
//         Console.WriteLine("\n ** Act Anjam Shod **");
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound?.Stage == GameStage.Night && state.Action == GameAction.Sleep)
//     {
//         Console.WriteLine($"\n {state.CurrentCard.GetType().Name} Bekhabad.");
//         Console.WriteLine("\n ** Khabid Shod **");
//         Console.ReadLine();
//         state = engine.Execute();
//             
//     }
//     else if (state.CurrentRound.NextStage == GameStage.Evening)
//     {
//         if (state.Action == GameAction.Pending)
//         {
//             Console.WriteLine("\n Press Enter To Start Voting . ");
//             Console.ReadLine();
//             state = engine.Execute();
//         }
//         else
//         {
//             Console.WriteLine($"\n Press Enter Vote For: [{state.CurrentPlayer?.User.Name}]");
//             var vote=  int.Parse((string.IsNullOrWhiteSpace(Console.ReadLine())?"0":Console.ReadLine()) ?? string.Empty);
//             if (state.CurrentPlayer != null) engine.Vote(state.CurrentPlayer, vote);
//             state = engine.Execute();
//         }
//         
//     }
//     
//     PrintInformation();
//    
// }
//
//  void PrintInformation()
// {
//     // Console.WriteLine($"TurnNumber : {state.CurrentPlayer?.TurnNumber} ");
//     // Console.WriteLine($"Player : {state.CurrentPlayer?.User.Name} ");
//     // Console.WriteLine($"Stage : {state.CurrentRound.Stage} ");
//     // Console.WriteLine($"Action : {state.Action} ");
// }


using Mafia.Engine;
using Mafia.Engine.Models;
using Mafia.Engine.Models.Scenarios;

Console.WriteLine("Please Choose Scenario To Start.");
Console.ReadLine();

var engine = new ScenarioEngine(new GodFatherScenario());
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

Console.WriteLine("Press Enter To Start . ");
Console.ReadLine();

engine.PrepareGame();

int order = 0;
while (true)
{
    var state = engine.Execute(order);
    var activity = state.Activity;

    switch (activity.Stage)
    {
        case StepStage.Morning when activity is { Type: StepType.GoodMorning}:
            Console.WriteLine("Sobh Bekheir Sohbat rO shoro mikonim ... :)");
            Console.WriteLine("## Shoro Sohbat ##");
            Console.ReadLine();
            break;
        
        // Day - Rooze Moarefeh Note
        case StepStage.Day when activity is { Type: StepType.Note, IsInterViewDay: true }:
            Console.WriteLine("Khob berim Sohbat ro Shoro knim ... :)");
            Console.WriteLine("## Berim ##");
            Console.ReadLine();
            break;
        
        // Day - Speaking
        case StepStage.Day when activity is { Audience:StepAudience.All, Type: StepType.Speak, Action: StepAction.LoopPlayer }:
            Console.WriteLine($"[{activity?.Player?.TurnNumber}] {activity?.Player?.User.Name} Dare harf Mizane");
            Console.WriteLine("## Nafar Badi ##");
            Console.ReadLine();
            break;
        
        // Evening - Rooze Moarefeh
        case StepStage.Evening when activity is { Type: StepType.Vote, Action: StepAction.None, IsInterViewDay: true }:
            Console.WriteLine($"Dar Roz Moarefeh Raygiri nadrim va mostagim be shab mirim");
            Console.WriteLine("## Shab Mishe ##");
            Console.ReadLine();
            break;
        
        // Evening - 
        case StepStage.Evening when activity is { Type: StepType.Vote, Action: StepAction.LoopPlayer, IsInterViewDay: false }:
            Console.WriteLine($"Ray Giri Baraye [{activity?.Player?.TurnNumber}] {activity?.Player?.User.Name}");
            Console.WriteLine("## Shab Mishe ##");
            Console.ReadLine();
            break;
        
        
        case StepStage.Night when activity is {Type:StepType.WakeUp , Audience:StepAudience.All}:
            Console.WriteLine($"Hame Bidar Beshavand");
            Console.WriteLine("## Hame Bidar Shodand ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.Sleep , Audience:StepAudience.All}:
            Console.WriteLine($"Hame Bekhaband");
            Console.WriteLine("## Hame Khabidand ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.WakeUp , Audience:StepAudience.MafiaGroup}:
            Console.WriteLine($"Mafia Ha Bidar Shavand");
            Console.WriteLine("## Mafia Ha Bidar Shodand ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.Sleep , Audience:StepAudience.MafiaGroup}:
            Console.WriteLine($"Mafia Ha Bkhaband");
            Console.WriteLine("## Khabidand ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.WakeUp , Audience:StepAudience.Mafia}:
            Console.WriteLine($"Mafia [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Bidar Behsavad");
            Console.WriteLine("## Bidar Shod ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.WakeUp , Audience: StepAudience.Citizen}:
            Console.WriteLine($"Shahrvand [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Bidar Behsavad");
            Console.WriteLine("## Bidar Shod ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.ShowLike , Audience:StepAudience.Mafia}:
            Console.WriteLine($"Mafia [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Like Neshan Dahad");
            Console.WriteLine("## Like Neshan Dad Shod ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.ShowLike , Audience: StepAudience.Citizen}:
            Console.WriteLine($"Shahrvand [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Like Neshan Dahad");
            Console.WriteLine("## Like Neshan Dad Shod ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.Sleep , Audience:StepAudience.Mafia}:
            Console.WriteLine($"Mafia [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Bekhabad");
            Console.WriteLine("## Khabidand ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.Sleep , Audience: StepAudience.Citizen}:
            Console.WriteLine($"Shahrvand [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Bekhabad");
            Console.WriteLine("## Khabidand ##");
            Console.ReadLine();
            break;

        case StepStage.Night when activity is {Type:StepType.NightAct , Audience:StepAudience.Mafia}:
            Console.WriteLine($"Mafia [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Act Khod ra anjam dahad");
            Console.WriteLine("## Anjam Dad ##");
            Console.ReadLine();
            break;
        
        case StepStage.Night when activity is {Type:StepType.NightAct , Audience: StepAudience.Citizen}:
            Console.WriteLine($"Shahrvand [{activity?.Player?.TurnNumber}][{activity?.Card?.GetType().Name}] {activity?.Player?.User.Name} Act Khod ra anjam dahad");
            Console.WriteLine("## Anjam Dad ##");
            Console.ReadLine();
            break;
        
        default:
            throw new ArgumentOutOfRangeException();
    }

    order = activity.Order;
}