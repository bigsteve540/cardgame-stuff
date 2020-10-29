using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Core;
using GameEngine.CardEffects;

namespace GameEngine.Conditions
{
    public class C_Haste : ICondition, IEffectOnBoardExit
    {
        public int ID => 2;
        public int Duration => -1;
        public int CurrentDuration { get; set; } = 0;
        public Turn_Phase TriggerPhase { get; }
        public Vector2 EffectTargetCoords { get; private set; }
        public int PlayerID { get; private set; }
        public Card Caster { get; private set; }

        public void Init(ref Vector2 _targetSlotCoords, ref int _playerID, ref Card _caster)
        {
            PlayerID = _playerID;
            EffectTargetCoords = _targetSlotCoords;
            Caster = _caster;

            Game.GetPlayer(PlayerID).Board.FetchSlot(_targetSlotCoords.x, _targetSlotCoords.y).ChangeCombatPrio(true);
            GameEventSystem.OnBoardExit += Trigger;
        }

        public void Trigger(int _casterID, Vector2 _coords)
        {
            if (PlayerID != _casterID)
                return;

            if (EffectTargetCoords != _coords)
                return;

            Game.GetPlayer(PlayerID).Board.FetchSlot(_coords.x, _coords.y).ChangeCombatPrio(false);
            GameEventSystem.OnBoardExit -= Trigger;
        }
    }
}
