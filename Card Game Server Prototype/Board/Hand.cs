using System.Collections.Generic;

namespace GameEngine.Core
{
    public class Hand
    {
        public static int HandSizeLimit => 10;
        public List<Card> Cards;

        public Hand()
        {
            Cards = new List<Card>();
        }

    }
}


