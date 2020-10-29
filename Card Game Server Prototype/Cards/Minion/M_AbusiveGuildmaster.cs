using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Cards
{
    public class M_AbusiveGuildmaster : MinionCard
    {
        public override string CardID => "M068";
        public override int CardEffectID => 6;
        public override string CardName => "Abusive Guildmaster";
        public override int CardCost => 7;
        public override Card_Archetype CardArchetype => Card_Archetype.Thief;

        public override int BaseAttack => 4;
        public override int BaseBarrier => 0;
        public override int BaseHealth => 5;

        public override bool CanAttack => true;
    }
}
