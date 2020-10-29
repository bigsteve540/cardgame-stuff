using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class Player
{
    public Board Board;
    public int ID;
    public string Username;

    public Player(int _id, string _username)
    {
        ID = _id;
        Username = _username;

        Board = new Board();
    }
}
