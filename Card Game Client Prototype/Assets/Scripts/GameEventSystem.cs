using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public static class GameEventSystem
{
    public delegate void turnPhaseTriggerHandler(int _playerID, Turn_Phase _currentPhase);
    public delegate void boardCoordinateHandler(Vector2 _coords);
    public delegate void cardStatModifiedHandler(Vector2 _coords, Card _caster);

    public static turnPhaseTriggerHandler OnChangeTurnPhase;

    public static boardCoordinateHandler OnBoardEnter;
    public static boardCoordinateHandler OnBoardExit;

    public static cardStatModifiedHandler OnUnitDeath;
    public static cardStatModifiedHandler OnTakeDamage;
    public static cardStatModifiedHandler OnHeal;
    public static boardCoordinateHandler OnUnitResurrect;


}

//CardEffectTrigger :: OnBoardEnter x, OnBoardExit x, OnDeath, OnResurrect, OnEnterHand
//TargetSide :: Friendly, Enemy
//TargetType :: Self, Other, Adjacent, Below, 
//AmountToTarget :: int (?)