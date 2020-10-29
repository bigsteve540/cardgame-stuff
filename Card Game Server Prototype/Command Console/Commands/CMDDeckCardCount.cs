using GameEngine.Core;
using System;

namespace GameEngine.Console.Commands
{
    public class CMDDeckCardCount : ConsoleCommand
    {
        public override string Name => "deckcount";
        public override string Usage => "{userID}";
        public override string Description => "Gets the size of {userID}'s deck";

        public override void Execute(string[] args)
        {
            Logger.Print($"Player {args[0]} deck size: {Game.GetPlayer(Convert.ToInt32(args[0])).Deck.Size()}");
        }
    }
}
