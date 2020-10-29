using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

[CreateAssetMenu(fileName = "New Environment", menuName = "Cards/Environment", order = 0)]
public class EnvironmentCard : Card
{
    [SerializeField] private string cardID = "$NULL$";
    public override string CardID { get { return cardID; }  }

    private Card_Type cardType = Card_Type.Environment;
    public override Card_Type CardType { get { return cardType; } }

    [SerializeField] private int cardEffectID = default;
    public override int CardEffectID { get { return cardEffectID; } }

    [Space]

    [SerializeField] private string cardName = default;
    public override string CardName { get { return cardName; } }

    [SerializeField] private int cardCost = default;
    public override int CardCost { get { return cardCost; } }

    [SerializeField] private Card_Species cardSpecies = Card_Species.None;
    public override Card_Species CardSpecies { get { return cardSpecies; } }

    [SerializeField] private Sprite cardImage = default;
    public override Sprite CardImage { get { return cardImage; } }

    [SerializeField] private string cardFlavourText = default;
    public override string CardFlavourText
    {
        get
        {
            if (CardEffectID > -1)
            {
                //return Game.CardEffectLibrary.GetEffectText(CardEffectID);
                return null;
            }
            return cardFlavourText;
        }
    }
}
