using System;
using GameEngine.Core;

namespace GameEngine.Console.Commands
{
    public class CMDShowHand : ConsoleCommand
    {
        public override string Name => "showhand";
        public override string Usage => "{userID}";
        public override string Description => "displays a player's hand";

        public override void Execute(string[] args)
        {
            for (int i = 0; i < Game.GetPlayer(Convert.ToInt32(args[0])).Hand.Cards.Count; i++)
            {
                Logger.Print(Game.GetPlayer(Convert.ToInt32(args[0])).Hand.Cards[i].ToString());
            }
        }
    }
}
