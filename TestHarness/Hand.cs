using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness
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
                Char[] CardValue = cardArray[i].ToCharArray();
                string value = CardValue[0].ToString();
                string suit = CardValue[1].ToString();
                Card card = new Card(value, suit);
                cards.Add(card);
            }

            this.m_sortedHand = cards;
        }

    }
}
