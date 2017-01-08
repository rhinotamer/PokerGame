using System.Collections.Generic;

namespace PokerGame
{
    public class HandToPlay
    {
        private TypeOfHand m_type;
        private List<Card> m_cards = new List<Card>();

        public TypeOfHand Type
        {
            get
            {
                return m_type;
            }
        }
        public List<Card> Cards
        {
            get
            {
                return m_cards;
            }
        }

        public HandToPlay() { }
        public HandToPlay(TypeOfHand handType, List<Card> cardsToPlay)
        {
            m_type = handType;
            m_cards = cardsToPlay;
        }
    }
}
