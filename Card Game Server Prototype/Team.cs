using System.Collections;
using System.Collections.Generic;
using GameEngine.Console;
using GameEngine.Network;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Core
{
    public class Team
    {
        public Player[] Players;

        public Team(int _numPlayers)
        {
            Players = new Player[_numPlayers];
            FetchPlayers(this, _numPlayers);
        }

        private static int targetID = 0;
        private static void FetchPlayers(Team _target, int _numPlayers)
        {
            for (int i = 0; i <= _numPlayers - 1; i++) //this won't work. need a uniformed way like the id assigner method || are u sure it seems like it works to me lol
            {
                _target.Players[i] = Server.Clients[targetID].Player;
                Logger.Print($"User {Server.Clients[targetID].Player.ID} assigned to a team.");
                targetID++;
            }
        }
    }
}
