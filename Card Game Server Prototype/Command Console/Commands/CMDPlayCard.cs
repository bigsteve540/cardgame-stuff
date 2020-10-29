using GameEngine.Core;
using System;
using System.Collections.Generic;

namespace GameEngine.Console.Commands
{
    public class CMDPlayCard : ConsoleCommand
    {
        public override string Name => "playcard";
        public override string Usage => "{userID} {cardNoInHand} {boardX} {boardY}";
        public override string Description => "plays a card to {boardX}{boardY} from {userID}'s hand";

        public override void Execute(string[] args)
        {
            List<int> allArgs = new List<int>();
            foreach (string arg in args)
            {
                allArgs.Add(Convert.ToInt32(arg));
            }

            Player p = Game.GetPlayer(allArgs[0]);

            if (p.PlayCard(allArgs[2], allArgs[3], p.Hand.Cards[allArgs[1]]))
            {
                Logger.Print($"Played card {args[1]} from player {args[0]} at slot {args[2]},{args[3]}");
            }
            else
            {
                Logger.Print($"Failed to play card {args[1]} from player {args[0]} at slot {args[2]},{args[3]}");
            }

        }
    }
}
