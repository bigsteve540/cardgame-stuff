using System.Collections.Generic;

namespace GameEngine.Console.Commands
{
    public class CMDHelp : ConsoleCommand
    {
        public override string Name => "help";
        public override string Usage => string.Empty;
        public override string Description => "helps :)";

        public override void Execute(string[] args)
        {
            foreach (KeyValuePair<string, ConsoleCommand> commands in ConsoleCommandHandler.Commands)
            {
                Logger.Print($"Command Name: {commands.Key}\n  Desc: {commands.Value.Description}\n  Usage: {(commands.Value.Usage == string.Empty ? "None" : commands.Value.Usage)}");
            }
        }
    }
}
