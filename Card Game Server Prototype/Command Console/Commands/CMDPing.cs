namespace GameEngine.Console.Commands
{
    public class CMDPing : ConsoleCommand
    {
        public override string Name => "ping";
        public override string Usage => string.Empty;
        public override string Description => "console returns 'pong!'";

        public override void Execute(string[] args)
        {
            Logger.Print("pong!");
        }
    }
}
