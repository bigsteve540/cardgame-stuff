using System.Collections.Generic;
using GameEngine.Conditions;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////
namespace GameEngine.Core
{
    public class Slot
    {
        public bool Occupied { get { return ActiveCard != null ? true:false; } }

        private int ownerID;

        private Card_Type[] allowedCardTypes;

        private Card activeCard = null;
        public Card ActiveCard { get { return activeCard; } private set { activeCard = value; } }

        public int Attack { get { return proxy.Attack; } }
        public bool CombatPriority { get { return proxy.HasCombatPriority; } }
        public bool IgnoresBarrier { get { return proxy.IgnoresBarrier; } }

        private CardStatProxy proxy;

        private List<Card> slotGrave;
        private Vector2 boardCoords;

        public Slot(Card_Type[] _allowed, Card _default, int _boardX, int _boardY, ref int _owner)
        {
            ownerID = _owner;
            allowedCardTypes = _allowed;
            ActiveCard = _default;
            boardCoords = new Vector2(_boardX, _boardY);
            slotGrave = new List<Card>();
            proxy = new CardStatProxy();
        }
        public Slot(Card_Type _allowed, Card _default, int _boardX, int _boardY, ref int _owner)
        {
            ownerID = _owner;
            allowedCardTypes = new Card_Type[1] { _allowed };
            ActiveCard = _default;
            boardCoords = new Vector2(_boardX, _boardY);
            slotGrave = new List<Card>();
            proxy = new CardStatProxy();
        }
        public Slot(Card_Type[] _allowed, int _boardX, int _boardY, ref int _owner) : this(_allowed, null, _boardX, _boardY, ref _owner) { }
        public Slot(Card_Type _allowed, int _boardX, int _boardY, ref int _owner) : this(_allowed, null, _boardX, _boardY, ref _owner) { }

        #region Slot Management
        public bool AddCard(Card _card)
        {
            if (!Occupied && ValidateCard(_card.CardType))
            {
                ActiveCard = _card;

                if (ActiveCard.CardEffectID != 0)
                {
                    ActiveCard.CardEffect = CardEffectHandler.GetEffect(ActiveCard.CardEffectID);
                    ActiveCard.CardEffect.Init(ref ownerID, ref boardCoords);
                }    

                if (ActiveCard is ICombatable)
                    proxy.ManageNewCard(ActiveCard as ICombatable);

                GameEventSystem.OnBoardEnter?.Invoke(ownerID, boardCoords);
                return true;
            }
            return false;
        }
        public bool RemoveCard()
        {
            if (Occupied)
            {
                GameEventSystem.OnBoardExit?.Invoke(ownerID, boardCoords);
                ActiveCard = null;
                return true;
            }
            return false;
        }

        public bool AddCardToGrave(Card _card)
        {
            if (ValidateCard(_card.CardType))
            {
                slotGrave.Add(_card);
                return true;
            }
            return false;
        }
        public bool RemoveCardFromGrave(Card _card)
        {
            if (ValidateCard(_card.CardType) && slotGrave.Remove(_card))
                return true;
            return false;
        }

        public void ResurrectUnit(int _indexInGrave)
        {
            if (!Occupied)
            {
                Card toRes = slotGrave[_indexInGrave];
                slotGrave.Remove(toRes);
                ActiveCard = toRes;
                GameEventSystem.OnUnitResurrect?.Invoke(ownerID, boardCoords);
            }
        }
        #endregion

        #region Proxy Interaction
        /// <summary>
        /// Cause the card within this slot to take damage.
        /// </summary>
        /// <param name="_value">The damage to deal.</param>
        /// <param name="_caster">The card causing the damage.</param>
        /// <returns>The new card health value. Returns -482 if card cannot be hurt.</returns>
        public int TakeDamage(int _value, Card _caster, bool _ignoreBarrier = false)
        {
            if (!(ActiveCard is ICombatable))
                return -482;

            int ignoreBarrier = _ignoreBarrier ? 0 : 1;

            if (((ignoreBarrier * proxy.Barrier) + proxy.Health) - _value <= 0)
            {
                if (AddCardToGrave(ActiveCard))
                    ActiveCard = null;
                GameEventSystem.OnUnitDeath?.Invoke(boardCoords, _caster, ref _value);
            }
            else
            {
                GameEventSystem.OnTakeDamage?.Invoke(boardCoords, _caster, ref _value);
                proxy.TakeDamage(_value, _ignoreBarrier);
            }
            return proxy.Health;
        }
        /// <summary>
        /// Cause the card within this slot to Heal.
        /// </summary>
        /// <param name="_value">The amount to heal.</param>
        /// <param name="_caster">The card causing the damage.</param>
        /// <returns>The new card health value. Returns -482 if card cannot be healed.</returns>
        public int Heal(int _value, Card _caster)
        {
            if (!(ActiveCard is ICombatable))
                return -482;

            if(proxy.Heal(_value))
                GameEventSystem.OnHeal?.Invoke(boardCoords, _caster, ref _value);

            return proxy.Health;
        }

        public int AddBarrier(int _value, Card _caster)
        {
            GameEventSystem.OnAddBarrier?.Invoke(boardCoords, _caster, ref _value);
            proxy.AddBarrier(_value);
            return proxy.Barrier;
        }
        public int HitBarrier(int _value, Card _caster)
        {
            GameEventSystem.OnHitBarrier?.Invoke(boardCoords, _caster, ref _value);
            proxy.HitBarrier(_value);
            return proxy.Barrier;
        }

        public int AddAttack(int _value, Card _caster)
        {
            GameEventSystem.OnAddAttack?.Invoke(boardCoords, _caster, ref _value);
            proxy.AddAttack(_value);
            return proxy.Attack;
        }
        public int ReduceAttack(int _value, Card _caster)
        {
            GameEventSystem.OnReduceAttack?.Invoke(boardCoords, _caster, ref _value);
            proxy.ReduceAttack(_value);
            return proxy.Attack;
        }

        public void ApplyCondition(ICondition _condition)
        {
            proxy.AttemptEntry(_condition, ref boardCoords, ref ownerID, ref activeCard);
        }

        public void LockAttack(bool _lock)
        {
            proxy.CanAttack = !_lock;
        }
        public void ChangeCombatPrio(bool _prio)
        {
            proxy.HasCombatPriority = _prio;
        }
        public void IgnoreBarrier(bool _ignore)
        {
            proxy.IgnoresBarrier = _ignore;
        }

        public int GetProxyValues(int _index)
        {
            switch (_index)
            {
                case 0:
                    return proxy.Attack;
                case 1:
                    return proxy.Barrier;
                case 2:
                    return proxy.Health;
                default:
                    return -1;
            }
        }
        #endregion

        private bool ValidateCard(Card_Type _type)
        {
            for (int i = 0; i < allowedCardTypes.Length; i++)
            {
                if (_type == allowedCardTypes[i])
                    return true;
            }
            return false;
        }
    }
}

