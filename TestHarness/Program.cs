using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawhand = "5C 3S 2H 4H 9C";
            Hand hand = new Hand(rawhand);

            List<Card> cards = hand.Cards;


            List<Card> result = GetSequence(cards);


            Console.Write("Type and key to exit...");            
            Console.ReadKey();


        }

        static List<Card> GetSequence(List<Card> input)
        {
            input = input.OrderBy(x => x.Number).ToList();

            var cards = new List<Card>();
            var seq = new List<Card>();
            foreach (var i in input)
            {
                int index = input.IndexOf(i);
                int nextNumber = ParseCardNumber(input[index + 1].Number);
                if (Convert.ToInt32(i.Number) + 1 == nextNumber)
                {
                    cards.Add(i);
                    if(cards.Count == 3)
                    {
                        cards.Add(input[index + 1]);
                        break;
                    }
                }
            }
            return cards;
        }

        static int ParseCardNumber(string cardNumber)
        {
            int result = 0;
            switch (cardNumber)
            {
                case "J":
                    result = 11;
                    break;
                case "Q":
                    result = 12;
                    break;
                case "K":
                    result = 13;
                    break;
                case "A":
                    result = 14;
                    break;
                default:
                    result = Convert.ToInt32(cardNumber);
                    break;
            }

            return result;
        }

    }
}
