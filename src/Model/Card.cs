namespace Model;

public class Card : IComparable<Card>
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }

    public static Card FromEncoding(string encoding)
    {
        encoding = encoding.ToUpper();

        Rank rank;
        Suit suit;

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

        suit = encoding[1] switch
        {
            'C' => Suit.Clubs,
            'D' => Suit.Diamonds,
            'H' => Suit.Hearts,
            'S' => Suit.Spades
        };

        return new Card() { Rank = rank, Suit = suit };
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
