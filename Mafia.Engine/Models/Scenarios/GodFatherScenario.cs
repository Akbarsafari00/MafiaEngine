using Mafia.Engine.Models.Cards;

namespace Mafia.Engine.Models.Scenarios;

public class GodFatherScenario : IScenario
{
    public bool hasInterviewDay => true;

    public List<Card> Cards => new()
    {
        new GodFatherCard(2),
        new CitizenCard(9),
        new ConstantineCard(8),
        new MatadorCard(3),
        new NostradamusCard(1),
        new CitizenKaneCard(7),
        new DrWatsonCard(5),
        new SaulGoodmanCard(4),
        new LeonTheProfessionalCard(6)
    };

    public List<Step> InterViewTemplates => new()
    {
        new Step(ActivityAction.None,(state , player) =>
        {
           
            state = new GameState()
            {
                Id = Guid.NewGuid(),
                Rounds = new List<Round>()
                {
                    new()
                    {
                        IsCurrentRound = true,
                        IsInterview = true,
                        RoundPlayers = state.Players.Select(x => new RoundPlayer()
                        {
                            Player = x,
                            IsTalked = false,
                            VoteCount = 0
                        }).ToList()
                    }
                },
                Message = "Bazi ro shoro knim ?",
                Stage = StepStage.Morning,
                Type = StepType.Note,
            };
        }),

        new Step(ActivityAction.None,(state , player) =>
        {
            state.Stage = StepStage.Day;
            state.Type = StepType.InterviewSpeak;
            state.Message = "Khob Mirim Baraye Sohbat haye Marefeh :)";
        }),

        new Step(ActivityAction.LoopPlayer,(state , player) =>
        {
            state.Stage = StepStage.Day;
            state.Type = StepType.InterviewSpeak;
            state.Message = "Khob Mirim Baraye Sohbat haye Marefeh :)";
        }),

        // new Step(StepAudience.All, StepAction.None, StepType.Vote, StepStage.Evening,
        //     isInterViewDay: true),
        //
        // new Step(StepAudience.All, StepAction.None, StepType.Sleep, StepStage.Night,
        //     isInterViewDay: true),
        //
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new NostradamusCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.NightAct,
        //     StepStage.Night, new NostradamusCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.Sleep, StepStage.Night,
        //     new NostradamusCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new GodFatherCard(0), isInterViewDay: true),
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new MatadorCard(0), isInterViewDay: true),
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new SaulGoodmanCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.ShowLike, StepStage.Night,
        //     new GodFatherCard(0), isInterViewDay: true),
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.ShowLike, StepStage.Night,
        //     new MatadorCard(0), isInterViewDay: true),
        // new Step(StepAudience.Mafia, StepAction.PlayerFromCard, StepType.ShowLike, StepStage.Night,
        //     new SaulGoodmanCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.MafiaGroup, StepAction.None, StepType.Sleep, StepStage.Night,
        //     isInterViewDay: true),
        //
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new CitizenKaneCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.ShowLike,
        //     StepStage.Night, new CitizenKaneCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.Sleep, StepStage.Night,
        //     new CitizenKaneCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new DrWatsonCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.ShowLike,
        //     StepStage.Night, new DrWatsonCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.Sleep, StepStage.Night,
        //     new DrWatsonCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new LeonTheProfessionalCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.ShowLike,
        //     StepStage.Night,
        //     new LeonTheProfessionalCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.Sleep, StepStage.Night,
        //     new LeonTheProfessionalCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.WakeUp, StepStage.Night,
        //     new ConstantineCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.ShowLike,
        //     StepStage.Night, new ConstantineCard(0), isInterViewDay: true),
        // new Step(StepAudience.Citizen, StepAction.PlayerFromCard, StepType.Sleep, StepStage.Night,
        //     new ConstantineCard(0), isInterViewDay: true),
        //
        // new Step(StepAudience.All, StepAction.None, StepType.WakeUp, StepStage.Night,
        //     isInterViewDay: true),
    };

    public List<Step> RoundTemplates => new()
    {
        // new Step(StepAudience.All, StepAction.None, StepType.GoodMorning, StepStage.Morning),
        // new Step(StepAudience.All, StepAction.LoopPlayer, StepType.Speak, StepStage.Day),
        // new Step(StepAudience.All, StepAction.LoopPlayer, StepType.Vote, StepStage.Evening),
        // new Step(StepAudience.All, StepAction.None, StepType.CheckVote, StepStage.Evening),
        // new Step(StepAudience.All, StepAction.LoopDefending, StepType.Defending, StepStage.Evening),
        // new Step(StepAudience.All, StepAction.LoopDefending, StepType.DefendingVote,
        //     StepStage.Evening),
        // new Step(StepAudience.All, StepAction.None, StepType.CheckDefendingVote, StepStage.Evening),
        // new Step(StepAudience.All, StepAction.None, StepType.LastChangeCard, StepStage.Evening),
    };
}