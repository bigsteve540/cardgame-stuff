using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////
public enum Game_Type { _1v1, _2v2, _4pFFA }
public enum Turn_Type { Alternate_Teams, Full_Team_Before_Pass, Random }
public static class Game
{

    public static Team[] Teams { get; private set; }
    private static Turn_Type turn_Type;

    private static TcpListener tcpListener;

    public static void StartGame(Game_Type _type, int _port)
    {
        switch (_type)
        {
            case Game_Type._1v1:
                //MaxPlayers = 2;
                Teams = new Team[2] { new Team(1), new Team(1) };
                break;
            case Game_Type._2v2:
                //MaxPlayers = 4;
                Teams = new Team[2] { new Team(2), new Team(2) };
                break;
            case Game_Type._4pFFA:
                //MaxPlayers = 4;
                Teams = new Team[4] { new Team(1), new Team(1), new Team(1), new Team(1) };
                break;
            default:
                Debug.LogError("Unsupported match type. Closing the match down.");
                Application.Quit();
                break;
        }


        //do networking shit here

    }
}
