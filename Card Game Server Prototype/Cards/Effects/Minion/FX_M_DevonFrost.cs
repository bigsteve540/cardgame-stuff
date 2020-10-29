using GameEngine.Conditions;
using GameEngine.Core;

namespace GameEngine.CardEffects
{
    public class FX_M_DevonFrost : CardEffect, IEffectOnBoardEnter
    {
        public override int EffectID => 4;
        public override string Description => "Freeze minions in adjacent columns on opponent's boards.";

        private int playerID;
        private C_Freeze freeze;

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            playerID = _player;
            freeze = new C_Freeze();
            GameEventSystem.OnBoardEnter += Trigger;
        }

        public void Trigger(int _casterID, Vector2 _coords)
        {
            if (_casterID != playerID)
                return;

            int[] targetIDs = Game.GetNonTeamIDs(_casterID);

            for (int i = 0; i < targetIDs.Length; i++)
            {
                Board enemyBoard = Game.GetPlayer(targetIDs[i]).Board;
                enemyBoard.FetchSlot(_coords.x - 1, 2).ApplyCondition(freeze);
                enemyBoard.FetchSlot(_coords.x + 1, 2).ApplyCondition(freeze);
            }

            GameEventSystem.OnBoardEnter -= Trigger;
        }
    }
}
