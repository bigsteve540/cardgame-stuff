using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
///////////////////////////////////////// 

public class ClientHandle
{
    public static void Welcome(Packet _packet)
    {
        string msg = _packet.ReadString();
        int id = _packet.ReadInt();

        Debug.Log($"Message from server: {msg}");
        Client.Instance.ID = id;
        ClientSend.WelcomeReceived();
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int id = _packet.ReadInt();
        string username = _packet.ReadString();

        Debug.Log("Attempting player spawn(?)");
    }
}
