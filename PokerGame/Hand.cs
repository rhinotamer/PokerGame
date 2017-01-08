using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class Hand
    {
        private string m_unsortedHand = string.Empty;
        private List<Card> m_sortedHand = null;
        
        public List<Card> Cards
        {
            get
            {
                return m_sortedHand;
            }
        }

        public Hand() { }
        public Hand(string hand)
        {
            SortHand(hand);
        }

        private void SortHand(string hand)
        {
            string[] cardArray = hand.Trim().Split(' ');
            List<Card> cards = new List<Card>();

            // add cards to hand
            for (int i = 0; i < cardArray.Length; i++)
            {
                Char[] cardValue = cardArray[i].ToCharArray();
                string value = cardValue[0].ToString();
                string suit = cardValue[1].ToString();
                Card card = new Card(value, suit);
                cards.Add(card);
            }

            // sort hand numercially
            this.m_sortedHand = cards.OrderBy(x => CardUtilities.ParseCardNumber(x.Number)).ToList();
        }

    }

    public enum TypeOfHand
    {
        NoHand = 1,
        OnePair = 2,
        TwoPairs = 3,
        ThreeOfAKind = 4,
        Straight = 5,
        Flush = 6,
        FullHouse = 7,
        FourOfAKind = 8,
        StraightFlush = 9,
        RoyalFlush = 10
    }

}
