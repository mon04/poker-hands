using Model;
using System.Linq;

namespace PokerLogic;

public static class HandDeterminer
{
    public static Hand GetHand(Card[] cards)
    {
        HandClass handClass;

        SortDescending(cards);

        if (TryFindRoyalFlush(cards))
        {
            handClass = HandClass.RoyalFlush;
        }
        else if (TryFindStraightFlush(cards))
        {
            handClass = HandClass.StraightFlush;
        }
        else if (TryFindFourOfAKind(cards))
        {
            handClass = HandClass.FourOfAKind;
        }
        else if (TryFindFullHouse(cards))
        {
            handClass = HandClass.FullHouse;
        }
        else if (TryFindFlush(cards))
        {
            handClass = HandClass.Flush;
        }
        else if (TryFindStraight(cards, true))
        {
            handClass = HandClass.Straight;
        }
        else if (TryFindThreeOfAKind(cards))
        {
            handClass = HandClass.ThreeOfAKind;
        }
        else if (TryFindTwoPair(cards))
        {
            handClass = HandClass.TwoPair;
        }
        else if (TryFindPair(cards))
        {
            handClass = HandClass.Pair;
        }
        else
        {
            handClass = HandClass.HighCard;
        }

        return new Hand() { Class = handClass, CompareOrder = cards };
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

    private static bool TryFindFlush(Card[] cards)
    {
        for (var i = 1; i < cards.Length; i++)
        {
            if (cards[i].Suit != cards[i - 1].Suit)
            {
                return false;
            }
        }
        return true;
    }

    private static bool TryFindStraight(Card[] cards, bool sortInCompareOrder)
    {
        // Check Five-High Straight:
        if (cards[0].Rank == Rank.Ace
            && cards[1].Rank == Rank.Five
            && cards[2].Rank == Rank.Four
            && cards[3].Rank == Rank.Three
            && cards[4].Rank == Rank.Two)
        {
            if(sortInCompareOrder)
            {
                Array.Sort(cards, (c1, c2) =>
                {
                    if (c1.Rank == Rank.Ace) return 1;
                    if (c2.Rank == Rank.Ace) return -1;
                    return -c1.CompareTo(c2);
                });
            }
            return true;
        }

        for (var i = 1; i < cards.Length; i++)
        {
            if (cards[i].Rank - cards[i - 1].Rank != -1)
            {
                return false;
            }
        }
        return true;
    }

    private static bool TryFindFullHouse(Card[] cards)
    {
        if (AreEqualInRank(cards[0], cards[1], cards[2])
            && !AreEqualInRank(cards[2], cards[3])
            && AreEqualInRank(cards[3], cards[4]))
        {
            var tripletRank = cards[0].Rank;
            Array.Sort(cards, (c1, c2) =>
            {
                if (c1.Rank == c2.Rank) return -c1.CompareTo(c2);
                if (c1.Rank == tripletRank) return -1;
                if (c2.Rank == tripletRank) return 1;
                return -c1.CompareTo(c2);
            });
            return true;
        }

        if (AreEqualInRank(cards[4], cards[3], cards[2])
            && !AreEqualInRank(cards[2], cards[1])
            && AreEqualInRank(cards[1], cards[0]))
        {
            var tripletRank = cards[4].Rank;
            Array.Sort(cards, (c1, c2) =>
            {
                if (c1.Rank == c2.Rank) return -c1.CompareTo(c2);
                if (c1.Rank == tripletRank) return -1;
                if (c2.Rank == tripletRank) return 1;
                return -c1.CompareTo(c2);
            });
            return true;
        }

        return false;
    }

    private static bool TryFindFourOfAKind(Card[] cards)
    {
        return TryFindDuplicates(cards, 4);
    }

    private static bool TryFindStraightFlush(Card[] cards)
    {
        return TryFindFlush(cards) && TryFindStraight(cards, true);
    }

    private static bool TryFindRoyalFlush(Card[] cards)
    {
        return (TryFindFlush(cards)
            && cards[0].Rank == Rank.Ace
            && cards[1].Rank == Rank.King
            && cards[2].Rank == Rank.Queen
            && cards[3].Rank == Rank.Jack
            && cards[4].Rank == Rank.Ten);
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

    private static bool AreEqualInRank(params Card[] cards)
    {
        for (var i = 1; i < cards.Length; i++)
        {
            if (cards[i].Rank != cards[i - 1].Rank) return false;
        }
        return true;
    }

    private static void SortDescending(Card[] cards)
    {
        Array.Sort(cards, (c1, c2) => - c1.CompareTo(c2));
    }
}
