using System;
using System.Collections.Generic;
using System.Text;

namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player();
            int player1NumberOfWins = 0;
            int player2NumberOfWins = 0;
            int gameCounter = 0;
            string filePath = @"C:\Users\nichj\OneDrive\Documents\Visual Studio 2015\Projects\PokerGame\poker.txt";


            Dealer dealer = new Dealer(filePath);
            dealer.DealCards();
            List<Game> games = dealer.Games;                

            foreach (Game game in games)
            {
                HandEvaluator evaluator = new HandEvaluator(game);
                string gameResultMsg = string.Empty;
                StringBuilder sb = new StringBuilder();

                evaluator.EvaluateGame(game);
                player1 = game.Player1;
                player2 = game.Player2;

                if(player1.IsWinner)
                {
                    player1NumberOfWins++;
                    gameResultMsg = string.Format("Winner: Player1 - {0}", player1.HandPlayed.Type);
                }
                else if(player2.IsWinner)
                {
                    player2NumberOfWins++;
                    gameResultMsg = string.Format("Winner: Player2 - {0}", player2.HandPlayed.Type);
                }

                foreach (var card in game.Player1.HandDealt.Cards)
                {
                    sb.AppendFormat("{0}{1} ", card.Number, card.Suit);                    
                }

                sb.Append("| ");

                foreach (var card in game.Player2.HandDealt.Cards)
                {
                    sb.AppendFormat("{0}{1} ", card.Number, card.Suit);
                }

                sb.AppendFormat(" =>  {0} ", gameResultMsg);

                Console.WriteLine(sb.ToString());
                Console.Write("");

                gameCounter++;

            }

            Console.WriteLine("Number of games played: " + gameCounter.ToString());
            Console.WriteLine(string.Format("Total Player 1 wins: {0}", player1NumberOfWins.ToString()));
            Console.WriteLine(string.Format("Total Player 2 wins: {0}", player2NumberOfWins.ToString()));            
            Console.ReadKey();
        }
    }
}
