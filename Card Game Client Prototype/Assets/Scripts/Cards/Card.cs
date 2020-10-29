using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public abstract class Card : ScriptableObject
{
    public abstract string CardID { get; }
    public abstract Card_Type CardType { get; }
    public abstract int CardEffectID { get; }

    public abstract string CardName { get; }
    public abstract int CardCost { get; }
    public abstract Card_Species CardSpecies { get; }
    public abstract Sprite CardImage { get; }
    public abstract string CardFlavourText { get; }
}