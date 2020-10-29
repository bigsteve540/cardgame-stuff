using System;
using System.Linq;

namespace GameEngine.Core
{
    public class GameSettings
    {
        public int MaxPlayers { get; private set; }

        public int TeamCount { get; private set; }
        public int[] PlayerCountPerTeam { get; private set; }

        public Game_Type Game_Type { get; set; }
        public Turn_Type TurnType { get; private set; }

        public GameSettings(Game_Type _gameType)
        {
            switch (_gameType)
            {
                case Game_Type._1v1:
                    MaxPlayers = 2;
                    TurnType = Turn_Type.Full_Team_Before_Pass;
                    break;
                case Game_Type._2v2:
                    MaxPlayers = 4;
                    TurnType = Turn_Type.Full_Team_Before_Pass;
                    break;
                case Game_Type._4pFFA:
                    MaxPlayers = 4;
                    TurnType = Turn_Type.Full_Team_Before_Pass;
                    break;
                case Game_Type._custom:
                    throw new Exception("Passed custom game settings using the wrong constructor.");
                default:
                    throw new Exception("Unhandled game type.");
            }
            Game_Type = _gameType;
        }
        public GameSettings(Turn_Type _turnType, int _teamCount, int[] _playersPerTeam, Game_Type _gameType = Game_Type._custom)
        {
            MaxPlayers = _playersPerTeam.Sum();
            TeamCount = _teamCount;
            PlayerCountPerTeam = _playersPerTeam;
            TurnType = _turnType;
            Game_Type = _gameType;
        }
    }
}
