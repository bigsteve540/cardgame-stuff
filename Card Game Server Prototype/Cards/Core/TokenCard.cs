/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public abstract class TokenCard : Card, ICombatable
{
    public abstract override string CardID { get; }
    public override Card_Type CardType => Card_Type.Token;
    public abstract override int CardEffectID { get; }
    public abstract override string CardName { get; }
    public abstract override int CardCost { get; }
    public abstract override Card_Archetype CardArchetype { get; }

    public TokenCard()
    {
        CardAttack = BaseAttack;
        CardBarrier = BaseBarrier;
        CardHealth = BaseHealth;
    }

    public int BaseAttack => 1;
    public int CardAttack { get; }

    public int BaseBarrier => 1;
    public int CardBarrier { get; }

    public int BaseHealth => 1;
    public int CardHealth { get; }

    public abstract bool CanAttack { get; }

    public override string ToString()
    {
        return base.ToString() + $"\nA:{CardAttack}\nB:{CardBarrier}\nH:{CardHealth}\n";
    }
}
