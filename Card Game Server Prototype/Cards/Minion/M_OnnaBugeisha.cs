namespace GameEngine.Cards
{
    public class M_OnnaBugeisha : MinionCard
    {
        public override string CardID => "M044";
        public override int CardEffectID => 2;
        public override string CardName => "Onna Bugeisha";
        public override int CardCost => 5;
        public override Card_Archetype CardArchetype => Card_Archetype.Samurai;

        public override int BaseAttack => 5;
        public override int BaseBarrier => 0;
        public override int BaseHealth => 5;

        public override bool CanAttack => true;
    }
}
