using GameEngine.Core;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Conditions
{
    public class C_Burn : ICondition, IStackable, IEffectOnPhaseChange
    {
        public int ID => 0;
        public int Duration => 2;
        public int CurrentDuration { get; set; } = 0;
        public Turn_Phase TriggerPhase => Turn_Phase.Standby;
        public int MaxStacks => 3;
        public int CurrentStacks { get; set; } = 0;

        public Vector2 EffectTargetCoords { get; private set; }
        public int PlayerID { get; private set; }
        public Card Caster { get; private set; }

        public void Init(ref Vector2 _targetCoords, ref int _playerID, ref Card _caster)
        {
            CurrentDuration = Duration;
            CurrentStacks = MaxStacks;

            PlayerID = _playerID;
            EffectTargetCoords = _targetCoords;
            Caster = _caster;

            GameEventSystem.OnChangeTurnPhase += Trigger;
        }

        public void Trigger(int _playerID, Turn_Phase _currentPhase)
        {
            if(PlayerID != _playerID && _currentPhase != TriggerPhase)
                return;

            Game.GetPlayer(_playerID).Board.FetchSlot(EffectTargetCoords.x, EffectTargetCoords.y)
                .TakeDamage(2, Caster);
            CurrentDuration--;

            if (Duration < 1)
                GameEventSystem.OnChangeTurnPhase -= Trigger;
        }
    }
}

