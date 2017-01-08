namespace PokerGame
{
    public class Player
    {
        public Hand HandDealt { get; set; }
        public HandToPlay HandPlayed { get; set; }
        public bool IsWinner { get; set; }
 
        public Player() { }
    }
}