using GameEngine.Core;
using System;

namespace GameEngine.Console.Commands
{
    public class CMDShowBoard : ConsoleCommand
    {
        public override string Name => "showboard";
        public override string Usage => "{userID}";
        public override string Description => "Displays a representation of {userID}'s board";

        public override void Execute(string[] args)
        {
            Logger.Print(Game.GetPlayer(Convert.ToInt32(args[0])).Board.ToString());
        }
    }
}
