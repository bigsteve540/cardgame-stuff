using GameEngine.Core;

namespace GameEngine.CardEffects
{
    public class FX_M_Underling : CardEffect, IEffectOnBoardEnter
    {
        public override int EffectID => 1;
        public override string Description => "Deal 2 damage to your Overlord when played.";

        private int playerID = -1;
        private Vector2 coords;

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            playerID = _player;
            coords = _coords;
            GameEventSystem.OnBoardEnter += Trigger;
        }

        public void Trigger(int _playerID, Vector2 _coords)
        {
            if (_playerID != playerID)
                return;
            if (coords != _coords)
                return;

            Card c = Game.GetPlayer(_playerID).Board.FetchSlot(_coords.x, _coords.y).ActiveCard;

            Game.GetPlayer(_playerID).Board.FetchSlot(5, 5).TakeDamage(2, c);
            GameEventSystem.OnBoardEnter -= Trigger;
        }
    }
}
