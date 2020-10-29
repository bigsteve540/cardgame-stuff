using System;

namespace GameEngine.Console.Commands
{
    public class CMDQuit : ConsoleCommand
    {
        public override string Name => "quit";
        public override string Usage => string.Empty;
        public override string Description => "closes the application with error code 0";

        public override void Execute(string[] args)
        {
            Environment.Exit(0);
        }
    }
}
