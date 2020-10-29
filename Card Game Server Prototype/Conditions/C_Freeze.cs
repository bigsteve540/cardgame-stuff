using GameEngine.Core;

namespace GameEngine.Conditions
{
    public class C_Freeze : ICondition, IEffectOnPhaseChange
    {
        public int ID => 1;
        public int Duration => 1;
        public int CurrentDuration { get; set; } = 0;
        public Turn_Phase TriggerPhase => Turn_Phase.Standby;
        public Vector2 EffectTargetCoords { get; private set; }
        public int PlayerID { get; private set; }
        public Card Caster { get; }

        public void Init(ref Vector2 _targetSlotCoords, ref int _playerID, ref Card _caster)
        {
            EffectTargetCoords = _targetSlotCoords;
            PlayerID = _playerID;

            CurrentDuration = Duration;

            Game.GetPlayer(PlayerID).Board.FetchSlot(EffectTargetCoords.x, EffectTargetCoords.y).LockAttack(true);
            GameEventSystem.OnChangeTurnPhase += Trigger;
        }

        public void Trigger(int _playerID, Turn_Phase _currentPhase)
        {
            if (PlayerID != _playerID && _currentPhase != TriggerPhase)
                return;

            CurrentDuration--;

            if(Duration < 1)
            {
                Game.GetPlayer(PlayerID).Board.FetchSlot(EffectTargetCoords.x, EffectTargetCoords.y).LockAttack(false);
                GameEventSystem.OnChangeTurnPhase -= Trigger;
            }
        }
    }
}
