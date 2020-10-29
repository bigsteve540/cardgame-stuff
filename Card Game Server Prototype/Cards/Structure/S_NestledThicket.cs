using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Cards
{
    public class S_NestledThicket : StructureCard
    {
        public override string CardID => "S005";
        public override int CardEffectID => 7;
        public override string CardName => "Nestled Thicket";
        public override int CardCost => 1;
        public override Card_Archetype CardArchetype => Card_Archetype.Beast;
        public override int BaseAttack => 0;
        public override int BaseBarrier => 0;
        public override int BaseHealth => 2;
        public override bool CanAttack => false;
    }
}
