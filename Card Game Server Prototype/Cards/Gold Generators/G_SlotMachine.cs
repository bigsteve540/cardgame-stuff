namespace GameEngine.Cards
{
    public class G_SlotMachine : GoldGenCard
    {
        public override string CardID => "G001";
        public override int CardEffectID => 5;
        public override string CardName => "The Slot Machine";
        public override int CardCost => 1;
        public override Card_Archetype CardArchetype => Card_Archetype.Casino;

        public override int BaseAttack => 0;
        public override int BaseBarrier => 7;
        public override int BaseHealth => 7;
    }
}
