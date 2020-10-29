using GameEngine.CardEffects;
using GameEngine.Core;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public abstract class Card
{
    public abstract string CardID { get; }
    public abstract Card_Type CardType { get; }
    public abstract int CardEffectID { get; }

    public abstract string CardName { get; }
    public abstract int CardCost { get; }
    public abstract Card_Archetype CardArchetype { get; }

    public CardEffect CardEffect { get; set; }

    public override string ToString()
    {
        return $" {CardID} | {CardName} | {CardCost}\n" +
               $"    {CardArchetype} | {CardType}\n" +
               $"{CardEffectHandler.GetEffectText(CardEffectID)}";
    }
}