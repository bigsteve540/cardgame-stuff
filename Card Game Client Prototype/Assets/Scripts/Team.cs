using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class Team
{
    public Player[] Players;

    public Team(int _numPlayers)
    {
        Players = new Player[_numPlayers];

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i] = new Player(PlayerIDAssigner.AssignPlayerID(), string.Empty /*no idea how the user's username is gonna end up here. Prolly sent by master server in a string or something?*/);
        }
    }

    private static class PlayerIDAssigner
    {
        private static int currentIDCount = 0;

        public static int AssignPlayerID()
        {
            currentIDCount++;
            return currentIDCount - 1;
        }
    }
}
