namespace GameEngine.Cards
{
    public class M_SamuraiInitiate : MinionCard
    {
        public override string CardID => "M002";
        public override int CardEffectID => 2;
        public override string CardName => "Samurai Initiate";
        public override int CardCost => 1;
        public override Card_Archetype CardArchetype => Card_Archetype.Samurai;

        public override int BaseAttack  => 1;
        public override int BaseBarrier => 0;
        public override int BaseHealth  => 1;

        public override bool CanAttack => true;
    }
}
