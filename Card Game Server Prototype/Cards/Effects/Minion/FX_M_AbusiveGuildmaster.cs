using GameEngine.Conditions;
using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.CardEffects
{
    public class FX_M_AbusiveGuildmaster : CardEffect, IEffectOnBoardEnter
    {
        public override int EffectID => 6;
        public override string Description => "Give all friendly Thief type cards Haste.";

        private int playerID { get; set; }

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            playerID = _player;

            GameEventSystem.OnBoardEnter += Trigger;
        }

        public void Trigger(int _casterID, Vector2 _coords)
        {
            if (playerID != _casterID)
                return;

            for (int y = 1; y < 2; y++) //targets structure and minion slots
            {
                for (int x = 2; x < 7; x++) //iterates across the columns
                {
                    Slot s = Game.GetPlayer(playerID).Board.FetchSlot(x, y);

                    if(s.ActiveCard.CardArchetype == Card_Archetype.Thief)
                        s.ApplyCondition(new C_Haste());
                }
            }
            GameEventSystem.OnBoardEnter -= Trigger;
        }
    }
}
