using System;
using System.Threading;
using GameEngine.Network;

namespace GameEngine.Core
{
    class Program
    {
        private static bool isRunning = false;
        private static Game_Type gameType = Game_Type._1v1; //this will be overwritten by matchmaking master server

        static void Main(string[] args)
        {
            System.Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.StartServer(gameType);
        }

        private static void MainThread()
        {
            Console.Logger.Print($"Main thread started. Running at {30} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    // If the time for the next loop is in the past, aka it's time to execute another tick
                    ThreadManager.UpdateMain();

                    _nextLoop = _nextLoop.AddMilliseconds(1000f/30f); // Calculate at what point in time the next tick should be executed

                    if (_nextLoop > DateTime.Now)
                    {
                        // If the execution time for the next tick is in the future, aka the server is NOT running behind
                        Thread.Sleep(_nextLoop - DateTime.Now); // Let the thread sleep until it's needed again.
                    }
                }
            }
        }
    }
}

