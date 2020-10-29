using GameEngine.Core;
using System;

namespace GameEngine.Console.Commands
{
    public class CMDShowOverlordHP : ConsoleCommand
    {
        public override string Name => "olordhp";
        public override string Usage => "{userID}";
        public override string Description => "Displays user {userID}'s overlord HP";

        public override void Execute(string[] args)
        {
            Logger.Print($"Overlord hp of player {args[0]}: {Game.GetPlayer(Convert.ToInt32(args[0])).GetOverlordHP()}");
        }
    }
}
