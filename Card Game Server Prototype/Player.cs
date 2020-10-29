/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

namespace GameEngine.Core
{
    public class Player
    {
        public int ID;
        public string Username;

        public int Gold;

        public Hand Hand;
        public Deck Deck;
        public Board Board;

        public Player(int _id, string _username, string[] _deckList)
        {
            ID = _id;
            Username = _username;

            Board = new Board(ref ID);
            Hand = new Hand();
            Deck = new Deck(_deckList, ref Hand.Cards);

            Deck.Draw(c => c.CardType == Card_Type.Overlord, 1);
            if(Hand.Cards.Count == 0)
                throw new System.Exception("Deck did not contain an Overlord.");
            PlayCard(5, 5, Hand.Cards[0] as OverlordCard);
        }

        public bool PlayCard(int _x, int _y, Card _target)
        {
            if (!Hand.Cards.Remove(_target))
                return false;

            return Board.FetchSlot(_x, _y).AddCard(_target);
        }

        public int GetOverlordHP()
        {
            return (Board.FetchSlot(5, 5).GetProxyValues(2));
        }
    }
}