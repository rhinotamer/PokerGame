using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    public static class CardUtilities
    {
        public static int GetTotalHandValue(List<Card> cards)
        {
            int result = 0;

            foreach (var h1 in cards)
            {
                result += ParseCardNumber(h1.Number);
            }

            return result;
        }

        public static int ParseCardNumber(string cardNumber)
        {
            int num = 0;
            switch (cardNumber)
            {
                case "T":
                    num = 10;
                    break;
                case "J":
                    num = 11;
                    break;
                case "Q":
                    num = 12;
                    break;
                case "K":
                    num = 13;
                    break;
                case "A":
                    num = 14;
                    break;
                default:
                    int result = 0;
                    if(Int32.TryParse(cardNumber, out result))
                    {
                        num = result;
                    }
                    break;
            }

            return num;
        }

        public static void CalculateTie(Game game)
        {
            TypeOfHand typeOfHand = game.Player1.HandPlayed.Type;
            switch (typeOfHand)
            {
                case TypeOfHand.Flush:
                case TypeOfHand.FullHouse:
                case TypeOfHand.TwoPairs:
                case TypeOfHand.OnePair:
                case TypeOfHand.NoHand:
                    {
                        GetWinnerByHighestCard(game);
                        break;
                    }

                case TypeOfHand.ThreeOfAKind:
                case TypeOfHand.FourOfAKind:
                    {
                        GetculateWinnerSameCardNumbers(game);
                        break;
                    }
                default:
                    break;

            }
            if (typeOfHand == TypeOfHand.Flush || typeOfHand == TypeOfHand.FullHouse || typeOfHand == TypeOfHand.OnePair || typeOfHand == TypeOfHand.TwoPairs)
            {
                GetWinnerByHighestCard(game);
            }
            if (typeOfHand == TypeOfHand.ThreeOfAKind || typeOfHand == TypeOfHand.FourOfAKind)
            {
                GetculateWinnerSameCardNumbers(game);
            }
        }

        public static void GetculateWinnerSameCardNumbers(Game game)
        {
            var p1Numbers = game.Player1.HandPlayed.Cards.Select(x => x.Number).ToList();
            var p2Numbers = game.Player2.HandPlayed.Cards.Select(x => x.Number).ToList();

            var p1 = Convert.ToInt32(p1Numbers[0]);
            var p2 = Convert.ToInt32(p2Numbers[0]);

            if (p1 > p2)
            {
                game.Player1.IsWinner = true;
            }
            if (p2 > p1)
            {
                game.Player2.IsWinner = true;
            }
        }

        public static void GetWinnerByHighestCard(Game game)
        {
            int p1HandValue = 0;
            int p2HandValue = 0;
            List<Card> p1Cards = game.Player1.HandPlayed.Cards;
            List<Card> p2Cards = game.Player2.HandPlayed.Cards;

            p1HandValue = CardUtilities.GetTotalHandValue(p1Cards);
            p2HandValue = CardUtilities.GetTotalHandValue(p2Cards);

            if (p1HandValue > p2HandValue)
            {
                game.Player1.IsWinner = true;
            }
            else if (p2HandValue > p1HandValue)
            {
                game.Player2.IsWinner = true;
            }
            else
            {
                Player winner = FindPlayerWithHighestCard(game.Player1, game.Player2);
                if (winner == game.Player1)
                {
                    game.Player1.IsWinner = true;
                }
                else
                {
                    game.Player2.IsWinner = true;
                }
            }
        }

        public static Player FindPlayerWithHighestCard(Player player1, Player player2)
        {
            Card p1HighestCard = GetHighCard(player1);
            Card p2HighestCard = GetHighCard(player2);

            if (CardUtilities.ParseCardNumber(p1HighestCard.Number) > CardUtilities.ParseCardNumber(p2HighestCard.Number))
            {
                return player1;

            }
            else if (CardUtilities.ParseCardNumber(p2HighestCard.Number) > CardUtilities.ParseCardNumber(p1HighestCard.Number))
            {
                return player2;
            }
            else
            {
                // if card numbers are equal add that card to the cards to play
                player1.HandPlayed.Cards.Add(p1HighestCard);
                player2.HandPlayed.Cards.Add(p2HighestCard);

                // execute recursive method until highest card is reached
                Player winner = FindPlayerWithHighestCard(player1, player2);
                return winner;
            }
        }

        public static Card GetHighCard(Player player)
        {
            Card result = null;
            int firstCard = 0;
            int nextCard = 0;
            List<Card> omitCards = player.HandPlayed.Cards;
            List<Card> allCards = player.HandDealt.Cards;

            foreach (var card in allCards)
            {
                if (omitCards.Contains(card))
                {
                    continue;
                }
                else
                {
                    if (result == null)
                    {
                        firstCard = ParseCardNumber(card.Number);
                        result = card;
                    }
                    else
                    {
                        nextCard = ParseCardNumber(card.Number);
                        if (nextCard > firstCard)
                        {
                            firstCard = nextCard;
                            result = card;
                        }
                    }
                }
            }
            return result;
        }

        public static List<Card> GetUnplayedCards(List<Card> allCards, List<Card> cardsToPlay)
        {
            var cards = new List<Card>();

            foreach (var card in allCards)
            {
                // skip played cards
                if (cardsToPlay.Contains(card))
                {
                    continue;
                }
                else
                {
                    cards.Add(card);
                }
            }
            return cards;
        }

        public static List<Card> GetConsecutiveValues(List<Card> input)
        {
            var cards = new List<Card>();
            var seq = new List<Card>();
            foreach (var i in input)
            {
                int index = input.IndexOf(i);
                int nextNumber = index < 4 ? CardUtilities.ParseCardNumber(input[index + 1].Number) : -1;
                if (CardUtilities.ParseCardNumber(i.Number) + 1 == nextNumber)
                {
                    cards.Add(i);
                    if (cards.Count == 4)
                    {
                        cards.Add(input[index + 1]);
                        break;
                    }
                }
            }
            return cards;
        }

    }
}
