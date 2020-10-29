using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.CardEffects
{ 
    public interface IEffectOnCombatStart
    {
        void TriggerStart(int _attackerID, Vector2 _attackerCoords, int _defenderID, Vector2 _defenderCoords);
    }
}
