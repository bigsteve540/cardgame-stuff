namespace GameEngine.CardEffects
{
    public interface IEffectOnBoardEnter
    {
        void Trigger(int _casterID, Vector2 _coords);
    }
}
