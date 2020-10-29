/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public abstract class MinionCard : Card, ICombatable
{
    public abstract override string CardID { get; }
    public override Card_Type CardType => Card_Type.Minion;
    public abstract override int CardEffectID { get; }
    public abstract override string CardName { get; }
    public abstract override int CardCost { get; }
    public abstract override Card_Archetype CardArchetype { get; }

    public MinionCard()
    {
        CardAttack = BaseAttack;
        CardBarrier = BaseBarrier;
        CardHealth = BaseHealth;
    }

    public abstract int BaseAttack { get; }
    public int CardAttack { get; }

    public abstract int BaseBarrier { get; }
    public int CardBarrier { get; }

    public abstract int BaseHealth { get; }
    public int CardHealth { get; }

    public abstract bool CanAttack { get; }
    
    public override string ToString()
    {
        return base.ToString() + $"\nA:{CardAttack}\nB:{CardBarrier}\nH:{CardHealth}\n";
    }
}
