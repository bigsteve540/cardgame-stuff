namespace GameEngine.Cards
{
    public class M_AddictedGambler : MinionCard
    {
        public override string CardID => "M070";
        public override int CardEffectID => 3;
        public override string CardName => "Addicted Gambler";
        public override int CardCost => 8;
        public override Card_Archetype CardArchetype => Card_Archetype.Casino;

        public override int BaseAttack => 4;
        public override int BaseBarrier => 4;
        public override int BaseHealth => 4;

        public override bool CanAttack => true;
    }
}
