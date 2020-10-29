namespace GameEngine.Cards
{
    public class O_Viirstraasa : OverlordCard
    {
        public override string CardID => "O001";
        public override int CardEffectID => 0;
        public override string CardName => "Viirstrasa";
        public override int CardCost => 0;
        public override Card_Archetype CardArchetype => Card_Archetype.Samurai;
        public override int BaseAttack => 0;
        public override int BaseBarrier => 0;
        public override int BaseHealth => 25;
    }
}
