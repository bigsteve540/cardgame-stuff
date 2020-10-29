using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class Slot
{
    public bool Occupied { get { if (activeCard != null) { return true; } return false; } }

    private Card_Type[] allowedCardTypes;
    private Card activeCard;

    private CardStatProxy proxy;

    private List<Card> slotGrave;
    private Vector2 boardCoords;

    public Slot(Card_Type[] _allowed, Card _default, int _boardX, int _boardY)
    {
        allowedCardTypes = _allowed;
        activeCard = _default;
        boardCoords = new Vector2(_boardX, _boardY);
        slotGrave = new List<Card>();
        proxy = new CardStatProxy();
    }
    public Slot(Card_Type _allowed, Card _default, int _boardX, int _boardY)
    {
        allowedCardTypes = new Card_Type[1] { _allowed };
        activeCard = _default;
        slotGrave = new List<Card>();
        proxy = new CardStatProxy();
        boardCoords = new Vector2(_boardX, _boardY);
    }
    public Slot(Card_Type[] _allowed, int _boardX, int _boardY) : this(_allowed, null, _boardX, _boardY) { }
    public Slot(Card_Type   _allowed, int _boardX, int _boardY) : this(_allowed, null, _boardX, _boardY) { }


    #region Slot Management
    public bool AddCard(Card _card)
    {
        if (!Occupied && ValidateCard(_card.CardType))
        {
            activeCard = _card;
            if (activeCard is ICombatable)
                proxy.ManageNewCard(activeCard as ICombatable);
            GameEventSystem.OnBoardEnter?.Invoke(boardCoords);
            return true;
        }
        return false;
    }
    public bool RemoveCard()
    {
        if (Occupied)
        {
            activeCard = null;
            GameEventSystem.OnBoardExit?.Invoke(boardCoords);
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
            activeCard = toRes;
            GameEventSystem.OnUnitResurrect?.Invoke(boardCoords);
        }
    }
    #endregion

    /// <summary>
    /// Cause the card within this slot to take damage.
    /// </summary>
    /// <param name="_value">The damage to deal.</param>
    /// <param name="_caster">The card causing the damage.</param>
    /// <returns>The new card health value. Returns -482 if card cannot be hurt.</returns>
    public int TakeDamage(int _value, Card _caster)
    {
        if (!(activeCard is ICombatable))
            return -482;

        if(proxy.Health - _value <= 0)
        {
            GameEventSystem.OnUnitDeath?.Invoke(boardCoords, _caster);
            return proxy.Health - _value;
        }
        else
        {
            proxy.Health -= _value;
            GameEventSystem.OnTakeDamage?.Invoke(boardCoords, _caster);
            return proxy.Health;
        }

    }
    /// <summary>
    /// Cause the card within this slot to Heal.
    /// </summary>
    /// <param name="_value">The amount to heal.</param>
    /// <param name="_caster">The card causing the damage.</param>
    /// <returns>The new card health value. Returns -482 if card cannot be healed.</returns>
    public int Heal(int _value, Card _caster)
    {
        if (!(activeCard is ICombatable))
            return -482;

        if(proxy.Health != (activeCard as ICombatable).CardHealth)
        {
            if (((activeCard as ICombatable).CardHealth - proxy.Health) >= _value)
            {
                proxy.Health += _value;
            }
            else if (((activeCard as ICombatable).CardHealth - proxy.Health) < _value)
            {
                proxy.Health = (activeCard as ICombatable).CardHealth;
            }
        }
        GameEventSystem.OnHeal?.Invoke(boardCoords, _caster);
        return proxy.Health;
    }

    public void ApplyCondition(ICondition _condition)
    {
        proxy.AttemptEntry(_condition);
    }

    private bool ValidateCard(Card_Type _type)
    {
        for (int i = 0; i < allowedCardTypes.Length; i++)
        {
            if(_type == allowedCardTypes[i])
                return true;
        }
        return false;
    }
}
