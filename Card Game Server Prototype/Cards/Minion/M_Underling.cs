namespace GameEngine.Cards
{
    public class M_Underling : MinionCard
    {
        public override string CardID => "M001";
        public override int CardEffectID => 1;
        public override string CardName => "Underling";
        public override int CardCost => 1;
        public override Card_Archetype CardArchetype => Card_Archetype.Demon;

        public override int BaseAttack  => 3;
        public override int BaseBarrier => 0;
        public override int BaseHealth  => 2;

        public override bool CanAttack => true;
    }
}
