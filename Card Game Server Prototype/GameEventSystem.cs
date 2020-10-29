/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Core
{
    public static class GameEventSystem
    {
        public delegate void TurnPhaseTriggerHandler(int _playerID, Turn_Phase _currentPhase);
        public delegate void BoardCoordinateHandler(int _playerID, Vector2 _coords);
        public delegate void CardStatModifiedHandler(Vector2 _coords, Card _caster, ref int _amountChanged);
        public delegate void DeckEmptyDrawAttemptHandler(int _playerID);
        public delegate void UserInputEffectTriggerHandler(int _casterID, int _targetID, int _effectID, Vector2 _targetXY);
        public delegate void CombatHandler(int _id1, Vector2 _coords1, int _id2, Vector2 _coords2);
        public delegate void TurnChangeHandler(int _id);

        public static CombatHandler OnCombatStart;
        public static CombatHandler OnCombatEnd;

        public static UserInputEffectTriggerHandler OnUserInputEffect;

        public static TurnChangeHandler OnTurnChange;

        public static TurnPhaseTriggerHandler OnChangeTurnPhase;
        public static DeckEmptyDrawAttemptHandler OnDrawFromEmptyDeck;

        public static BoardCoordinateHandler OnBoardEnter;
        public static BoardCoordinateHandler OnBoardExit;

        public static CardStatModifiedHandler OnUnitDeath;

        public static CardStatModifiedHandler OnTakeDamage;
        public static CardStatModifiedHandler OnHeal;

        public static CardStatModifiedHandler OnAddBarrier;
        public static CardStatModifiedHandler OnHitBarrier;

        public static CardStatModifiedHandler OnAddAttack;
        public static CardStatModifiedHandler OnReduceAttack;

        public static BoardCoordinateHandler OnUnitResurrect;
    }
}


//CardEffectTrigger :: OnBoardEnter x, OnBoardExit x, OnDeath, OnResurrect, OnEnterHand
//TargetSide :: Friendly, Enemy
//TargetType :: Self, Other, Adjacent, Below, 
//AmountToTarget :: int (?)