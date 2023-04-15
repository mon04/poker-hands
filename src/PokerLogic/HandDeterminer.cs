using Model;

namespace PokerLogic;

public static class HandDeterminer
{
    public static bool TryFindRoyalFlush(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        if (!IsFlush(cards)) return false;

        if (cards[0].Rank != Rank.Ace) return false;

        rank = Rank.Ace;
        return true;
    }

    public static bool TryFindStraightFlush(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        if (!IsStraight(cards)) return false;

        if (!IsFlush(cards)) return false;

        rank = cards[0].Rank;
        return true;
    }

    public static bool TryFindFourOfAKind(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        return TryFindDuplicates(cards, 4, out rank, out kickerRanks);
    }

    private static bool TryFindFullHouse(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks= Array.Empty<Rank>();

        if (cards[0] == cards[1] && cards[1] == cards[2] && cards[2] != cards[3] && cards[3] == cards[4])
        {
            rank = cards[0].Rank;
            return true;
        }

        if (cards[0] == cards[1] && cards[1] != cards[2] && cards[2] == cards[3] && cards[3] == cards[4])
        {
            rank = cards[2].Rank;
            return true;
        }

        return false;
    }

    public static bool TryFindFlush(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        if(!IsFlush(cards))
        {
            return false;
        }

        rank = cards[0].Rank;
        kickerRanks = cards[1..].Select(card => card.Rank).ToArray();
        Array.Sort(kickerRanks, ((r1, r2) => - r1.CompareTo(r2)));
        return true;
    }

    public static bool TryFindStraight(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        if (IsStraight(cards))
        {
            return false;
        }

        // Check for a Five-High Straight:
        if (cards[0].Rank == Rank.Ace && cards[1].Rank == Rank.Five)
        {
            rank = Rank.Five;
            return true;
        }

        rank = cards[0].Rank;
        return true;
    }

    public static bool TryFindThreeOfAKind(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        return TryFindDuplicates(cards, 3, out rank, out kickerRanks);
    }

    public static bool TryFindTwoPair(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        int lowRankInt = -1;
        int highRankInt = -1;

        var ranksFound = new int[Enum.GetValues(typeof(Rank)).Length];

        foreach(var card in cards)
        {
            ranksFound[(int)card.Rank]++;
        }

        for(var i = ranksFound.Length - 1; i >= 0; i--)
        {
            if (ranksFound[i] == 2)
            {
                if(highRankInt == -1)
                {
                    highRankInt = i;
                    continue;
                }
                lowRankInt = i;
                break;
            }
        }

        if(lowRankInt == -1 || highRankInt == -1)
        {
            return false;
        }

        Rank lowRank = (Rank) lowRankInt;
        Rank highRank = (Rank) highRankInt;
        rank = highRank;

        foreach(var card in cards)
        {
            if(card.Rank != lowRank && card.Rank != highRank)
            {
                kickerRanks = new[] { lowRank, card.Rank };
                return true;
            }
        }

        return false;
    }

    public static bool TryFindPair(Card[] cards, out Rank rank, out Rank[] kickerRanks)
    {
        return TryFindDuplicates(cards, 2, out rank, out kickerRanks);
    }

    private static bool IsFlush(Card[] cards)
    {
        for (var i = 1; i < cards.Length; i++)
        {
            if (cards[i].Suit != cards[i - 1].Suit) return false;
        }
        return true;
    }

    private static bool IsStraight(Card[] cards)
    {
        var i = 1;

        if (cards[0].Rank == Rank.Ace)
        {
            i = 2;
        }

        for (; i < cards.Length; i++)
        {
            if (cards[i - 1].Rank - cards[i].Rank != -1) return false;
        }

        return true;
    }

    private static bool TryFindDuplicates(Card[] cards, int numberOfDupes, out Rank rank, out Rank[] kickerRanks)
    {
        rank = default(Rank);
        kickerRanks = Array.Empty<Rank>();

        var ranksChecked = new HashSet<Rank>();

        for(var i = 0; i < cards.Length - 1; i++)
        {
            var c1 = cards[i];
            var cardsOfThisRank = 1;

            if(ranksChecked.Contains(c1.Rank)) continue;

            for(var j = i + 1; j < cards.Length; j++)
            {
                var c2 = cards[j];

                if (c2.Rank != c1.Rank) break;

                if(++cardsOfThisRank == numberOfDupes)
                {
                    rank = c1.Rank;
                    kickerRanks = cards.Where(card => card.Rank != c1.Rank)
                        .Select(card => card.Rank).ToArray();
                    Array.Sort(kickerRanks, ((card1, card2) => - card1.CompareTo(card2)));
                    return true;
                }
            }

            ranksChecked.Add(c1.Rank);
        }

        return false;
    }
}