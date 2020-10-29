using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class ClientSend
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.Instance.Tcp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        Debug.Log("Welcome received. Sending ack to server.");
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.Instance.ID);
            packet.Write("bob");
            packet.Write("O001|G001|G001|M001|M001|M001|M001|M001|M070|M070|M070|M070|M070|M070|M070|M070|M070|M070");

            SendTCPData(packet);
        }
    }
    #endregion
}
