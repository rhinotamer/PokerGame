using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public class HandEvaluator
    {
        Game m_game;

        public HandEvaluator(Game game)
        {
            m_game = game;
        }

        public void EvaluateGame(Game game)
        {
            // get type of hand and cards to play
            game.Player1.HandPlayed = GetHandToPlay(game.Player1.HandDealt.Cards);
            game.Player2.HandPlayed = GetHandToPlay(game.Player2.HandDealt.Cards);


            // check if both players have the same type of hand, and if so calculate the tie
            if (game.Player2.HandPlayed.Type == game.Player1.HandPlayed.Type)
            {
                CardUtilities.CalculateTie(game);
            }
            else
            {
                // get the type of hands from the dictionary key and compare enum values to determine winner
                var p1Hand = game.Player1.HandPlayed;
                var p2Hand = game.Player2.HandPlayed;

                if (p1Hand.Type > p2Hand.Type)
                {
                    game.Player1.IsWinner = true;
                }
                else
                {
                    game.Player2.IsWinner = true;
                }
            }
        }

        public HandToPlay GetHandToPlay(List<Card> cards)
        {
            HandToPlay result = null;
            var returnCards = new List<Card>();

            if (IsRoyalFlush(cards, out returnCards))
            {
                // royal flush
                result = new HandToPlay(TypeOfHand.RoyalFlush, returnCards);
            }
            else if (IsStraightFlush(cards, out returnCards))
            {
                // straight flush
                result = new HandToPlay(TypeOfHand.StraightFlush, returnCards);
            }
            else if (IsFourOfAKind(cards, out returnCards))
            {
                // four of a kind
                result = new HandToPlay(TypeOfHand.FourOfAKind, returnCards);
            }
            else if (IsFlush(cards, out returnCards))
            {
                // flush
                result = new HandToPlay(TypeOfHand.Flush, returnCards);
            }
            else if (IsStraight(cards, out returnCards))
            {
                // straight
                result = new HandToPlay(TypeOfHand.Straight, returnCards);
            }
            else if (IsThreeOfAKind(cards, out returnCards))
            {
                var unplayedCards = CardUtilities.GetUnplayedCards(cards, returnCards);
                if (IsOnePair(unplayedCards, out returnCards))
                {
                    // full house
                    result = new HandToPlay(TypeOfHand.FullHouse, returnCards);
                }
                else
                {
                    // three of a kind
                    result = new HandToPlay(TypeOfHand.ThreeOfAKind, returnCards);
                }
            }
            else if (IsTwoPairs(cards, out returnCards))
            {
                // two pair
                result = new HandToPlay(TypeOfHand.TwoPairs, returnCards);
            }
            else if (IsOnePair(cards, out returnCards))
            {
                // one pair
                result = new HandToPlay(TypeOfHand.OnePair, returnCards);
            }
            else
                result = new HandToPlay(TypeOfHand.NoHand, returnCards);

            return result;
        }

        private bool IsOnePair(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            returnCards = new List<Card>();
            List<string> cardNumbers = new List<string>();

            var pair = (from c in cards
                        join c2 in cards on c.Number equals c2.Number
                        where c != c2
                        orderby c.Number
                        select c).ToArray();

            for (int i = 0; i < pair.Length; i++)
            {
                cardNumbers.Add(pair[i].Number);
            }

            var distinctCards = cardNumbers.Distinct().ToArray();

            // only one pair found
            if (distinctCards.Length == 1)
            {
                for (int i = 0; i < pair.Length; i++)
                {
                    returnCards.Add(pair[i]);
                }
                result = true;
            }

            return result;
        }

        private bool IsTwoPairs(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            returnCards = new List<Card>();
            var cardNumbers = new List<string>();

            var pair = (from c in cards
                        join c2 in cards on c.Number equals c2.Number
                        where c != c2
                        orderby c.Number
                        select c).ToArray();

            for (int i = 0; i < pair.Length; i++)
            {
                cardNumbers.Add(pair[i].Number);
            }

            var distinctCards = cardNumbers.Distinct().ToArray();

            // two pair found
            if (distinctCards.Length == 2)
            {
                for (int i = 0; i < pair.Length; i++)
                {
                    returnCards.Add(pair[i]);
                }
                result = true;
            }

            return result;
        }

        private bool IsThreeOfAKind(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            returnCards = new List<Card>();

            var groups = cards.GroupBy(x => x.Number);
            foreach (var group in groups)
            {
                var groupedCards = new List<Card>();
                foreach (var groupedItem in group)
                {
                    groupedCards.Add(groupedItem);
                }

                if (groupedCards.Count() == 3)
                {
                    foreach (var card in groupedCards)
                    {
                        returnCards.Add(card);
                    }

                    result = true;
                }
            }
            return result;
        }

        private bool IsStraight(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            
            // get sequence of 5 consecutive cards if it exists
            returnCards = CardUtilities.GetConsecutiveValues(cards);
            if (returnCards.Count == 5)
            {
                result = true;
            }

            return result;
        }

        private bool IsFlush(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            returnCards = new List<Card>();
            
            // get distinct suits into array
            var suits = cards.Select(x => x.Suit).Distinct().ToArray();
            if (suits.Length == 1)
            {
                result = true;
                returnCards = cards;
            }
            return result;
        }

        private bool IsFourOfAKind(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;
            returnCards = new List<Card>();

            var groups = cards.GroupBy(x => x.Number);
            foreach (var group in groups)
            {
                var groupedCards = new List<Card>();
                foreach (var groupedItem in group)
                {
                    groupedCards.Add(groupedItem);
                }

                if (groupedCards.Count() == 4)
                {
                    foreach (var card in groupedCards)
                    {
                        returnCards.Add(card);
                    }
                    result = true;
                }
            }
            return result;
        }

        private bool IsStraightFlush(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;

            // get sequence of 5 consecutive cards if it exists
            returnCards = CardUtilities.GetConsecutiveValues(cards);
            if (returnCards.Count == 5)
            {
                var suits = returnCards.Select(x => x.Suit).Distinct().ToArray();
                if (suits.Length == 1)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool IsRoyalFlush(List<Card> cards, out List<Card> returnCards)
        {
            bool result = false;

            // get sequence of 5 consecutive cards if it exists
            returnCards = CardUtilities.GetConsecutiveValues(cards);
            if (returnCards.Count == 5 && CardUtilities.GetTotalHandValue(returnCards) == 60)
            {
                var suits = returnCards.Select(x => x.Suit).Distinct().ToArray();
                if (suits.Length == 1)
                {
                    result = true;                  
                    }
            }


            return result;
        }

        
    }
}