using System.Collections.Generic;
using System.Text;

namespace PokerGame
{
    public class Dealer
    {
        private string filepath = string.Empty;

        public List<Game> Games { get; set; }

        public Dealer(string path)
        {
            filepath = path;
            Games = new List<Game>();
        }

        public void DealCards()
        {
            foreach (string hand in ReadHandsFromFile())
            {
                Game game = new Game();
                Player player1 = new Player();
                Player player2 = new Player();

                game.Player1 = player1;
                game.Player2 = player2;

                var cardList = hand.Split(' ');

                // deal player 1
                StringBuilder sb1 = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    sb1.Append(cardList[i] + " ");
                }
                string hand_1 = sb1.ToString();
                Hand handPlayer1 = new Hand(hand_1);
                game.Player1.HandDealt = handPlayer1;

                // deal player 2
                StringBuilder sb2 = new StringBuilder();
                for (int i = 5; i < 10; i++)
                {
                    sb2.Append(cardList[i] + " ");
                }
                string hand_2 = sb2.ToString();
                Hand handPlayer2 = new Hand(hand_2);
                game.Player2.HandDealt = handPlayer2;

                this.Games.Add(game);
            }
        }

        private List<string> ReadHandsFromFile()
        {
            var hands = new List<string>();
            string line = string.Empty;

            System.IO.StreamReader file = new System.IO.StreamReader(filepath);

            while ((line = file.ReadLine()) != null)
            {
                hands.Add(line);
            }

            return hands;
        }


    }

}
