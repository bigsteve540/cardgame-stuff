using System;
using System.Net.Sockets;
using GameEngine.Core;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Network
{
    public class Client
    {
        public static int dataBufferSize = 4096;

        public bool Claimed = false;
        public int ID;
        public string[] DeckList;
        public string Username;
        public Player Player;
        public TCP Tcp;

        public Client(int _clientId)
        {
            ID = _clientId;
            Tcp = new TCP(ID);
        }

        public TcpClient FetchTCPSocket()
        {
            return Tcp.Socket;
        }

        public void ConnectViaTCP(TcpClient _client)
        {
            Tcp.Connect(_client);
        }

        public class TCP
        {
            public TcpClient Socket;

            private readonly int id;
            private NetworkStream stream;
            private Packet receivedData;
            private byte[] receiveBuffer;

            public TCP(int _id)
            {
                id = _id;
            }

            public void Connect(TcpClient _socket)
            {
                Socket = _socket;
                Socket.ReceiveBufferSize = dataBufferSize;
                Socket.SendBufferSize = dataBufferSize;

                stream = Socket.GetStream();

                receivedData = new Packet();
                receiveBuffer = new byte[dataBufferSize];

                stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(ReceiveCallback), null);

                ServerSend.Welcome(id, "Welcome to the server!");
                Console.Logger.Print("Client successfully bound, sending welcome message.");
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                try
                {
                    int _byteLength = stream.EndRead(_result);
                    if (_byteLength <= 0)
                    {
                        Server.Clients[id].Disconnect();
                        return;
                    }

                    byte[] _data = new byte[_byteLength];
                    Array.Copy(receiveBuffer, _data, _byteLength);

                    receivedData.Reset(HandleData(_data)); // Reset receivedData if all data was handled
                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, new AsyncCallback(ReceiveCallback), null);
                }
                catch (Exception _ex)
                {
                    Console.Logger.Print($"Error receiving TCP data: {_ex}");
                    Server.Clients[id].Disconnect();
                }
            }

            public void SendData(Packet _packet)
            {
                try
                {
                    if (Socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null); // Send data to appropriate client
                    }
                }
                catch (Exception _ex)
                {
                    Console.Logger.Print($"Error sending data to player {id} via TCP: {_ex}");
                }
            }

            private bool HandleData(byte[] _data)
            {
                int _packetLength = 0;

                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                        return true;
                }

                while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Server.PacketHandlers[_packetId](id, _packet);
                        }
                    });

                    _packetLength = 0;
                    if (receivedData.UnreadLength() >= 4)
                    {
                        _packetLength = receivedData.ReadInt();
                        if (_packetLength <= 0)
                            return true; 
                    }
                }

                if (_packetLength <= 1)
                    return true;

                return false;
            }

            public void Disconnect()
            {
                Socket.Close();
                stream = null;
                receivedData = null;
                receiveBuffer = null;
                Socket = null;
            }
        }

        private void Disconnect()
        {
            Console.Logger.Print($"{Tcp.Socket.Client.RemoteEndPoint} has disconnected.");

            Player = null;

            Tcp.Disconnect();

        }
    }
}
