using System.Collections.Generic;
using GameEngine.Conditions;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////
namespace GameEngine.Core
{
    public class CardStatProxy
    {
        public int Attack { get; private set; }
        public int Barrier { get; private set; }
        public int Health { get; private set; }
        public bool CanAttack { get; set; }
        public bool HasCombatPriority { get; set; }
        public bool IgnoresBarrier { get; set; }

        private ICombatable card;
        private List<ICondition> activeConditions;

        public CardStatProxy()
        {
            Attack = -1;
            Barrier = -1;
            Health = -1;

            activeConditions = new List<ICondition>();
        }

        public void ManageNewCard(ICombatable _card)
        {
            card = _card;

            Attack = card.CardAttack;
            Barrier = card.CardBarrier;
            Health = card.CardHealth;
            CanAttack = card.CanAttack;
        }

        public void AttemptEntry(ICondition _condition, ref Vector2 _slotXY, ref int _playerID, ref Card _caster)
        {
            ICondition match = activeConditions.Find(x => x.ID == _condition.ID);

            if (match != null && match is IStackable)
            {
                IStackable temp = match as IStackable;

                if (temp.CurrentStacks < 1)
                    match.Init(ref _slotXY, ref _playerID, ref _caster);
                else if(temp.CurrentStacks < temp.MaxStacks)
                    temp.CurrentStacks++;
            }
            else
            {
                activeConditions.Add(_condition);
                _condition.Init(ref _slotXY, ref _playerID, ref _caster);
            }
        }

        public void TakeDamage(int _value, bool _ignoreBarrier = false)
        {
            int tmp = ((_ignoreBarrier ? 0 : 1) * Barrier) + Health - _value;

            if (tmp > Health && !_ignoreBarrier)
                Barrier = tmp - Health;
            else
            {
                Barrier = _ignoreBarrier ? Barrier : 0;
                Health = tmp;
            }
        }
        public bool Heal(int _value)
        {
            if (Health != card.CardHealth)
            {
                Health = (Health + _value >= card.BaseHealth) ? card.BaseHealth : Health + _value;
                return true;
            }
            return false;
        }

        public void AddBarrier(int _value)
        {
            Barrier += _value;
        }
        public void HitBarrier(int _value)
        {
            if (Barrier - _value < 0)
                Barrier = 0;
            else
            Barrier -= _value;
        }

        public void AddAttack(int _value)
        {
            Attack += _value;
        }
        public void ReduceAttack(int _value)
        {
            if (Attack - _value < 0)
                Attack = 0;
            else
            Attack -= _value;
        }
    }
}

