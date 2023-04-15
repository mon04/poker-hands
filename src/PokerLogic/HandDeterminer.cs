using Model;
using System.Linq;

namespace PokerLogic;

public static class HandDeterminer
{
    public static Hand GetHand(Card[] cards)
    {
        SortByRankDescending(cards);

        if (TryFindTwoPair(cards))
        {
            return new Hand() { Class = HandClass.TwoPair, CompareOrder = cards };
        }

        if (TryFindPair(cards))
        {
            return new Hand() { Class = HandClass.Pair, CompareOrder = cards };
        }

        return new Hand() { Class = HandClass.HighCard, CompareOrder = cards };
    }

    private static bool TryFindPair(Card[] cards)
    {
        return TryFindDuplicates(cards, 2);
    }

    private static bool TryFindTwoPair(Card[] cards)
    {
        var amtOfRankFound = new int[Enum.GetValues(typeof(Rank)).Length];

        int highPairRank = -1;
        int lowPairRank = -1;

        foreach(Card card in cards)
        {
            amtOfRankFound[(int) card.Rank]++;

            if (amtOfRankFound[(int) card.Rank] == 2)
            {
                if(highPairRank == -1)
                {
                    highPairRank = (int) card.Rank;
                    continue;
                }

                lowPairRank = (int)card.Rank;
                Array.Sort(cards, (c1, c2) =>
                {
                    if (c1.Rank == c2.Rank) return - c1.CompareTo(c2);
                    if ((int)c1.Rank == highPairRank) return -1;
                    if ((int)c2.Rank == highPairRank) return 1;
                    if ((int)c1.Rank == lowPairRank) return -1;
                    if ((int)c2.Rank == lowPairRank) return 1;
                    return - c1.CompareTo(c2);
                });
                return true;
            }
        }
        return false;
    }

    private static bool TryFindThreeOfAKind(Card[] cards)
    {
        return TryFindDuplicates(cards, 3);
    }


    private static bool TryFindFourOfAKind(Card[] cards)
    {
        return TryFindDuplicates(cards, 4);
    }

    private static bool TryFindDuplicates(Card[] cards, int numberOfDupes)
    {
        var amtOfRankFound = new int[Enum.GetValues(typeof(Rank)).Length];

        foreach (Card card in cards)
        {
            if (++amtOfRankFound[(int)card.Rank] == numberOfDupes)
            {
                Array.Sort(cards, (c1, c2) =>
                {
                    if (c1.Rank == c2.Rank) return - c1.CompareTo(c2);
                    if (c1.Rank == card.Rank) return -1;
                    if (c2.Rank == card.Rank) return 1;
                    return - c1.CompareTo(c2);
                });
                return true;
            }
        }

        return false;
    }

    private static void SortByRankDescending(Card[] cards)
    {
        Array.Sort(cards, (c1, c2) => - c1.CompareTo(c2));
    }
}
