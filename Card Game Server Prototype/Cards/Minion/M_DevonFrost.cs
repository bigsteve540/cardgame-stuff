namespace GameEngine.Cards
{
    public class M_DevonFrost : MinionCard
    {
        public override string CardID => "M003";
        public override int CardEffectID => 4;
        public override string CardName => "Devon Frost";
        public override int CardCost => 1;
        public override Card_Archetype CardArchetype => Card_Archetype.Demon;

        public override int BaseAttack => 1;
        public override int BaseBarrier => 0;
        public override int BaseHealth => 1;

        public override bool CanAttack => true;
    }
}
