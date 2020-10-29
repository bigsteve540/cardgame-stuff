using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using GameEngine.Core;
using GameEngine.Console;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Network
{
    public static class Server
    {
        public static bool Full { get; set; } = false;
        public static int Port { get; private set; } = -1;

        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, Client> Clients = new Dictionary<int, Client>();
        public static Dictionary<int, PacketHandler> PacketHandlers;

        private static TcpListener tcpListener;

        public static void StartServer(Game_Type _gameType)
        {
            Game.Settings = new GameSettings(Game_Type._1v1);
            Port = AttemptFetchPort();

            Logger.Print("Starting networking protocols...");
            InitServerData();

            Logger.Print($"Server started on port {Port}. Awaiting connections...");

            Logger.Print("Now Allowing User Input.");
            ConsoleCommandHandler.Init();
        }

        private static int AttemptFetchPort()
        {
            int currentTestingPort = 49152;
            bool successfulBind = false;

            while (!successfulBind)
            {
                tcpListener = new TcpListener(IPAddress.Any, currentTestingPort);
                try
                {
                    tcpListener.Start();
                    successfulBind = true;
                }
                catch
                {
                    currentTestingPort++;
                    tcpListener = null;
                }

                if(currentTestingPort > 65535)
                {
                    Logger.Print("No valid port found. Aborting startup.");
                    Environment.Exit(ErrorCodes.NO_PORTS_AVAILABLE);
                }
            }
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);
            return currentTestingPort;
            //TODO: tell matchmaking server the port number
        }

        private static void InitServerData()
        {
            for (int i = 0; i < Game.Settings.MaxPlayers; i++)
            {
                Clients.Add(i, new Client(i));
            }

            PacketHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived }
            };
            Logger.Print("Initialized packet handlers.");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);
            Logger.Print($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            if (!Full)
            {
                for (int i = 0; i <= Game.Settings.MaxPlayers; i++)
                {
                    if (Clients[i].FetchTCPSocket() == null)
                    {
                        Logger.Print("Found a client with an empty TCP socket. Binding...");
                        Clients[i].ConnectViaTCP(_client);
                        return;
                    }
                }
            }
            else
            {
                Logger.Print($"{_client.Client.RemoteEndPoint} failed to connect: Server full!");
            }
        }
    }
}

