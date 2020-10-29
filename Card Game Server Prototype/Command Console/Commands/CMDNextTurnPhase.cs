namespace GameEngine.Console.Commands
{
    public class CMDNextTurnPhase : ConsoleCommand
    {
        public override string Name => "nextphase";
        public override string Usage => string.Empty;
        public override string Description => "pushes the turn handler onto the next turn phase";

        public override void Execute(string[] args)
        {
            Core.TurnHandler.NextPhase();
        }
    }
}
