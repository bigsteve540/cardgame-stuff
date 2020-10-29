using GameEngine.Core;

namespace GameEngine
{
    public interface IEffectOnPhaseChange
    {
        void Trigger(int _playerID, Turn_Phase _currentPhase);
    }
}
