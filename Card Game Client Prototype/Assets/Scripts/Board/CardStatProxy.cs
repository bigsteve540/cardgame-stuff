using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public class CardStatProxy
{
    public int Attack  { get; set; }
    public int Barrier { get; set; }
    public int Health  { get; set; }

    private ICombatable card;
    private List<ICondition> activeConditions;

    public CardStatProxy()
    {
        Attack = 0;
        Barrier = 0;
        Health = 0;

        activeConditions = new List<ICondition>();
    }

    public void ManageNewCard(ICombatable _card)
    {
        card = _card;

        Attack = card.CardAttack;
        Barrier = card.CardBarrier;
        Health = card.CardHealth;
    }

    public void AttemptEntry(ICondition _condition)
    {
        //check if list contains that condition already
        //check if it stacks
        //if it stacks add a new one

        ICondition match = activeConditions.Find(x => x.ID == _condition.ID);

        if (match != null)
        {
            if (match is IStackable)
            {
                IStackable temp = match as IStackable;

                if (temp.CurrentStacks < temp.MaxStacks)
                    temp.CurrentStacks++;
            }
        }
        else
        {
            activeConditions.Add(_condition);
            //_condition.Init();
        }
    }
}
