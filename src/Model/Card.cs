namespace Model;

public class Card : IComparable<Card>
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
    public string Title { get => $"{Rank} of {Suit}"; }
    public string Encoding
    {
        get
        {
            char rankSymbol = Rank switch
            {
                Rank.Ace   => 'A',
                Rank.King  => 'K',
                Rank.Queen => 'Q',
                Rank.Jack  => 'J',
                Rank.Ten   => 'T',
                Rank.Nine  => '9',
                Rank.Eight => '8',
                Rank.Seven => '7',
                Rank.Six   => '6',
                Rank.Five  => '5',
                Rank.Four  => '4',
                Rank.Three => '3',
                Rank.Two   => '2'
            };

            char suitSymbol = Suit switch
            {
                Suit.Clubs    => 'c',
                Suit.Diamonds => 'd',
                Suit.Hearts   => 'h',
                Suit.Spades   => 's'
            };

            return $"{rankSymbol}{suitSymbol}";
        }
    }
    public static Card FromEncoding(string encoding)
    {
        if(encoding == null || encoding.Length != 2)
        {
            throw new ArgumentException($"{nameof(encoding)} must have length 2 (a rank symbol followed by a suit symbol)");
        }

        encoding = encoding.ToUpper();

        Rank? rank = null;
        Suit? suit = null;

        rank = encoding[0] switch
        {
            '2' => Rank.Two,
            '3' => Rank.Three,
            '4' => Rank.Four,
            '5' => Rank.Five,
            '6' => Rank.Six,
            '7' => Rank.Seven,
            '8' => Rank.Eight,
            '9' => Rank.Nine,
            'T' => Rank.Ten,
            'J' => Rank.Jack,
            'Q' => Rank.Queen,
            'K' => Rank.King,
            'A' => Rank.Ace
        };

        if(rank == null)
        {
            throw new ArgumentException($"Unable to determine rank from symbol: {encoding[0]}");
        }

        suit = encoding[1] switch
        {
            'C' => Suit.Clubs,
            'D' => Suit.Diamonds,
            'H' => Suit.Hearts,
            'S' => Suit.Spades
        };

        if (suit == null)
        {
            throw new ArgumentException($"Unable to determine rank from symbol: {encoding[0]}");
        }

        return new Card() { Rank = (Rank) rank, Suit = (Suit) suit };
    }

    public int CompareTo(Card? other)
    {
        if(other is null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        int rankComparison = this.Rank.CompareTo(other.Rank);

        if(rankComparison == 0)
        {
            return this.Suit.CompareTo(other.Suit);
        }

        return rankComparison;
    }
}
