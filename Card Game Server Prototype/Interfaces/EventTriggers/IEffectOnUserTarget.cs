namespace GameEngine.CardEffects
{
    public interface IEffectOnUserTarget
    {
        void Trigger(int _casterID, int _targetID, int _effectID, Vector2 _targetXY);
    }
}
