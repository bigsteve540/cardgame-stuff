using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public interface ICondition
{
    int ID { get; }
    int Duration { get; }
    int CurrentDuration { get; set; }
    Turn_Phase TriggerPhase { get; }
    Card EffectCaster { get; }

    void Init(Card _caster);
    void Destroy();

    void Trigger(Slot _targetSlot); 
}

public interface IStackable
{
    int MaxStacks { get; }
    int CurrentStacks { get; set; }
}
