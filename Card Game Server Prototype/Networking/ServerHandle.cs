using GameEngine.Core;
using GameEngine.Console;

namespace GameEngine.Network
{
    public static class ServerHandle
    {
        public static void WelcomeReceived(int _clientID, Packet _packet)
        {
            int clientIdCheck = _packet.ReadInt();
            string username = _packet.ReadString();
            string deckList = _packet.ReadString();

            Server.Clients[_clientID].DeckList = deckList.Split('|');

            Logger.Print($"Ack received from {Server.Clients[_clientID].Tcp.Socket.Client.RemoteEndPoint}. Assigning ID: {_clientID}.");
            if (_clientID != clientIdCheck)
            {
                Logger.Print($"User: {username} (ID: {_clientID}) has assumed the wrong client ID ({clientIdCheck})!");
            }

            Server.Clients[_clientID].Claimed = true;

            bool triggered = false;
            for (int i = 1; i <= Server.Clients.Count; i++)
            {
                if (!Server.Clients[i - 1].Claimed)
                {
                    triggered = true;
                    break;
                }
            }

            if (!triggered)
            {
                Game.StartGame();
                Server.Full = true;
            }
        }

        public static void CardAttackCard(int _clientID, Packet _packet)
        {
            int otherID = _packet.ReadInt();
            Vector2 coords = _packet.ReadVector2();
            Vector2 otherCoords = _packet.ReadVector2();

            Game.StartCombat(_clientID, coords, otherID, otherCoords);
        }
    }
}
