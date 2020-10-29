using GameEngine.Core;

namespace GameEngine.CardEffects
{
    public class FX_M_AddictedGambler : CardEffect, IEffectOnBoardEnter
    {
        public override int EffectID => 3;
        public override string Description => "All your unspent gold is added to this card's attack";

        private int playerID;
        private Vector2 coords;

        public override void Init(ref int _player, ref Vector2 _coords)
        {
            playerID = _player;
            coords = _coords;
            GameEventSystem.OnBoardEnter += Trigger;
        }

        public void Trigger(int _playerID, Vector2 _coords)
        {
            if ( playerID != _playerID)
                return;
            if (coords != _coords)
                return;

            Player p = Game.GetPlayer(_playerID);
            Card c = p.Board.FetchSlot(_coords.x, _coords.y).ActiveCard;

            int x = p.Board.FetchSlot(_coords.x, _coords.y).AddAttack(p.Gold, c);
            p.Gold = 0;

            GameEventSystem.OnBoardEnter -= Trigger;
        }
    }
}
