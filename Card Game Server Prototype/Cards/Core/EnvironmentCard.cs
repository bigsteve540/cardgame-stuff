﻿/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public abstract class EnvironmentCard : Card
{
    public abstract override string CardID { get; }
    public override Card_Type CardType => Card_Type.Environment;
    public abstract override int CardEffectID { get; }
    public abstract override string CardName { get; }
    public abstract override int CardCost { get; }
    public abstract override Card_Archetype CardArchetype { get; }
}
