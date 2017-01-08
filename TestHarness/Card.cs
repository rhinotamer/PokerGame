namespace TestHarness
{
    public class Card
    {
        public string Number { get; set; }
        public string Suit { get; set; }

        public Card() { }
        public Card(string value, string suit)
        {
            this.Number = value;
            this.Suit = suit;
        }

    }
}