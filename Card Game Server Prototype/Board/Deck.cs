using System;
using System.Collections.Generic;

namespace GameEngine.Core
{
    public class Deck
    {
        private List<Card> cards;
        private List<Card> targetHand;

        public Deck(string[] _cardIDs, ref List<Card> _targetHand)
        {
            cards = new List<Card>();
            targetHand = _targetHand;
            for (int i = 0; i < _cardIDs.Length; i++)
            {
                cards.Add(CardLibrary.Library[_cardIDs[i]]);
            }
            Shuffle();
        }

        public void AddCardToDeck(Card _target)
        {
            cards.Add(_target);
            Shuffle();
        }

        public bool RemoveCardFromDeck(Card _target)
        {
            return cards.Remove(_target) ? true : false;
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                Card tmp = cards[i];
                int randi = rand.Next(i, cards.Count);
                cards[i] = cards[randi];
                cards[randi] = tmp;
            }
        }

        public void Draw(Predicate<Card> _match, int _searches) //searches deck based on predicate
        {
            for (int i = 0; i <= _searches - 1; i++)
            {
                if (cards.Count <= 0)
                {
                    GameEventSystem.OnDrawFromEmptyDeck?.Invoke(TurnHandler.TargetIDofTurn);
                    continue;
                }

                try
                {
                    Card target = cards.Find(_match);
                    if(target != null)
                    {
                        targetHand.Add(target);
                        RemoveCardFromDeck(target);
                    }
                }
                catch
                {
                    /*all is good, i'm just being lazy. predicate didn't find a card and threw an exception*/
                }
            }
        }

        public void Draw(int _searches) //assumed to be random
        {

            Random rand = new Random();
            try
            {
                for (int i = 0; i <= _searches - 1; i++)
                {
                    if (cards.Count <= 0)
                    {
                        GameEventSystem.OnDrawFromEmptyDeck?.Invoke(TurnHandler.TargetIDofTurn);
                        continue;
                    }

                    int Rindex = rand.Next(0, cards.Count);
                    Card target = cards[Rindex];
                    targetHand.Add(target);
                    RemoveCardFromDeck(target);
                }
            }
            catch
            {
                /*should use this and above catch block to check if the deck is empty, then fire an event accordingly*/
            }

        }

        #region Helper Methods
        public int Size()
        {
            return cards.Count;
        }
        #endregion
    }
}


