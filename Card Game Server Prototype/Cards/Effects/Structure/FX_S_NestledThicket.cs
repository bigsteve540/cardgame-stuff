using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.CardEffects
{
    public class FX_S_NestledThicket : CardEffect, IEffectOnTurnChange
    {
        public override int EffectID => 7;
        public override string Description => "Gain +1 Attack for your opponent's turn.";

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            throw new NotImplementedException();
        }

        public void Trigger(int _id)
        {
            throw new NotImplementedException();
        }
    }
}
