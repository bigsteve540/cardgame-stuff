using System.Collections.Generic;
using GameEngine.Network;
using GameEngine.Console;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Core
{
    public enum Game_Type { _1v1, _2v2, _4pFFA, _custom }

    public static class Game
    {
        public static Team[] Teams { get; private set; }
        public static GameSettings Settings { get; set; }

        public static void StartGame()
        {
            Logger.Print("Loading Card Library...");
            CardLibrary.InitLib();
            Logger.Print("Loading Card Effects...");
            CardEffectHandler.Init();
            Logger.Print("Preparing Players...");
            for (int i = 0; i < Server.Clients.Count; i++)
            {
                Server.Clients[i].Player = new Player(Server.Clients[i].ID, Server.Clients[i].Username, Server.Clients[i].DeckList);
            }
            Logger.Print("Preparing Teams...");
            switch (Settings.Game_Type)
            {
                case Game_Type._1v1:
                    Teams = new Team[2] { new Team(1), new Team(1) };
                    break;
                case Game_Type._2v2:
                    Teams = new Team[2] { new Team(2), new Team(2) };
                    break;
                case Game_Type._4pFFA:
                    Teams = new Team[4] { new Team(1), new Team(1), new Team(1), new Team(1) };
                    break;
                case Game_Type._custom:
                    Teams = new Team[Settings.TeamCount];
                    for (int i = 0; i < Settings.TeamCount; i++)
                    {
                        Teams[i] = new Team(Settings.PlayerCountPerTeam[i]);
                    }
                    break;
                default:
                    Logger.Print("Unsupported match type. Closing the match down.");
                    break;
            }
            Logger.Print("Assigning Events...");
            InitCoreGameEvents();
            Logger.Print("Initializing Turn Order...");
            TurnHandler.InitTurnOrder(Teams, new GameSettings(Game_Type._1v1));
            Logger.Print("Done. \nGame started.");
            TurnHandler.StartTurn(0);
        }

        public static void StartCombat(int _attackerID, Vector2 _attackerCoords, int _defenderID, Vector2 _defenderCoords)
        {
            if (TurnHandler.CurrentPhase != Turn_Phase.ComCalc)
                return;

            GameEventSystem.OnCombatStart?.Invoke(_attackerID, _attackerCoords, _defenderID, _defenderCoords);

            Slot attacker = GetPlayer(_attackerID).Board.FetchSlot(_attackerCoords.x, _attackerCoords.y);
            Slot defender = GetPlayer(_defenderID).Board.FetchSlot(_defenderCoords.x, _defenderCoords.y);
                
            if(attacker.CombatPriority == defender.CombatPriority)
            {
                attacker.TakeDamage(defender.Attack, defender.ActiveCard, defender.IgnoresBarrier);
                defender.TakeDamage(attacker.Attack, attacker.ActiveCard, attacker.IgnoresBarrier);
            }
            else if(attacker.CombatPriority)
            {
                defender.TakeDamage(attacker.Attack, attacker.ActiveCard, attacker.IgnoresBarrier);
                if (defender.ActiveCard != null)
                    attacker.TakeDamage(defender.Attack, defender.ActiveCard, defender.IgnoresBarrier);
            }
            else
            {
                attacker.TakeDamage(defender.Attack, defender.ActiveCard, defender.IgnoresBarrier);
                if (attacker.ActiveCard != null)
                    defender.TakeDamage(attacker.Attack, attacker.ActiveCard, attacker.IgnoresBarrier);
            }
            GameEventSystem.OnCombatEnd?.Invoke(_attackerID, _attackerCoords, _defenderID, _defenderCoords);
        }


        private static void InitCoreGameEvents()
        {

        }

        public static int[] GetNonTeamIDs(int _playerID)
        {
            List<int> master = new List<int>();

            bool match = false;
            for (int t = 0; t < Teams.Length; t++)
            {
                match = false;
                List<int> tmpIDLst = new List<int>();

                for (int p = 0; p < Teams[t].Players.Length; p++)
                {
                    tmpIDLst.Add(Teams[t].Players[p].ID);
                    if (Teams[t].Players[p].ID == _playerID)
                        match = true;
                }

                if (!match)
                    master.AddRange(tmpIDLst);
            }

            return master.ToArray();
        }

        public static int GetPlayerCount()
        {
            return Server.Clients.Count;
        }
        public static Player GetPlayer(int _id)
        {
            return Server.Clients[_id].Player;
        }
    }
}
