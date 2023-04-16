namespace Model;

public class Hand : IComparable<Hand>
{
    public HandClass Class { get; set; }
    public Card[] CompareOrder { get; set; }

    public int CompareTo(Hand? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException("other");
        }

        if (this.Class == other.Class)
        {
            for (var i = 0; i < CompareOrder.Length; i++)
            {
                var thisCard = this.CompareOrder[i];
                var otherCard = other.CompareOrder[i];

                int cardComparison = thisCard.CompareTo(otherCard);

                if (cardComparison == 0) continue;

                return cardComparison;
            }
        }

        return this.Class.CompareTo(other.Class);
    }
}
