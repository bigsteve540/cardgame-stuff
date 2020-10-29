namespace GameEngine.CardEffects
{
    public abstract class CardEffect
    {
        public abstract int EffectID { get; }
        public abstract string Description { get; }

        public abstract void Init(ref int _player, ref Vector2 _coords);
    }
}
