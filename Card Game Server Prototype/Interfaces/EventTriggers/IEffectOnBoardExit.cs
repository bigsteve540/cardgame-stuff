using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.CardEffects
{
    public interface IEffectOnBoardExit
    {
        void Trigger(int _casterID, Vector2 _coords);
    }
}
