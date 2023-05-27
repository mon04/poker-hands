using Model;

namespace PokerLogic;

public static class DeckLogic
{
    public static Queue<Card> GetDeck()
    {
        var suits = Enum.GetValues(typeof(Suit));
        var ranks = Enum.GetValues(typeof(Rank));

        var deck = new Queue<Card>(52);

        int i = 0;
        foreach(Suit suit in suits)
        {
            foreach (Rank rank in ranks)
            {
                deck.Enqueue(new Card(suit, rank));
            }
        }

        return deck;
    }

    public static Queue<Card> Shuffle(Queue<Card> cards)
    {
        var cardsArray = cards.ToArray();

        // Fisher-Yates algorithm https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

        var rng = new Random();

        for(int i = cardsArray.Length - 1; i >= 1; i--)
        {
            var j = rng.Next(i);
            var temp = cardsArray[i];
            cardsArray[i] = cardsArray[j];
            cardsArray[j] = temp;
        }

        var q = new Queue<Card>();
        foreach(Card card in cardsArray)
        {
            q.Enqueue(card);
        }

        return q;
    }
}
