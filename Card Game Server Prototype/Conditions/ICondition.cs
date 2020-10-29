using GameEngine.Core;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Conditions
{
    public interface ICondition
    {
        int ID { get; }
        int Duration { get; }
        int CurrentDuration { get; set; }
        Turn_Phase TriggerPhase { get; }
        Vector2 EffectTargetCoords { get; }
        int PlayerID { get; }
        Card Caster { get; }

        void Init(ref Vector2 _targetSlotCoords, ref int _playerID, ref Card _caster);
    }

    public interface IStackable
    {
        int MaxStacks { get; }
        int CurrentStacks { get; set; }
    }
}

