using Model;

namespace PokerLogic;

public static class HandDeterminer
{
	public static Hand GetBestHand(Card[] cards)
	{
		if (cards.Length != 7)
		{
			throw new ArgumentException($"{nameof(cards)} must have length 7.");
		}

		Hand? bestHand = null;

		for(int i=0; i < cards.Length; i++)
		{
			for(var j=i+1; j < cards.Length; j++)
			{
				for(int k=j+1; k < cards.Length; k++)
				{
					for(int m=k+1; m < cards.Length; m++)
					{
						for(int n=m+1; n < cards.Length; n++)
						{
							Card[] combination = new Card[5];
							combination[0] = cards[i];
							combination[1] = cards[j];
							combination[2] = cards[k];
							combination[3] = cards[m];
							combination[4] = cards[n];

							Hand combinationHand = ClassifyHand(combination);

							if(bestHand is null || combinationHand.CompareTo(bestHand) > 0)
							{
								bestHand = combinationHand;
							}
						}
					}
				}
			}
		}

		return bestHand;
	}

	private static Hand ClassifyHand(Card[] cards)
	{
		SortDescending(cards);

		var cardsByRank = GetCardsByRank(cards);
		var cardsBySuit = GetCardsBySuit(cards);


		// 01. RoyalFlush

		if (TryFindRoyalFlush(cards, cardsBySuit, out var isFlush))
		{
			return new Hand() { Class = HandClass.RoyalFlush, CompareOrder = cards };
		}

		// 02. StraightFlush

		if (isFlush && TryFindStraight(cards, out var compareOrder))
		{
			return new Hand() { Class = HandClass.StraightFlush, CompareOrder = compareOrder };
		}

		// 03. FourOfAKind

		if (TryFindFourOfAKind(cards, cardsByRank, out compareOrder))
		{
			return new Hand() { Class = HandClass.FourOfAKind, CompareOrder = compareOrder };
		}

		// 04. FullHouse

		if (TryFindFullHouse(cards, cardsByRank, out var threeOfAKindRank, out var pairRank, out compareOrder))
		{
			return new Hand() { Class = HandClass.FullHouse, CompareOrder = compareOrder };
		}

		// 05. Flush

		if (isFlush)
		{
			return new Hand() { Class = HandClass.Flush, CompareOrder = cards };
		}

		// 06. Straight

		if (TryFindStraight(cards, out compareOrder))
		{
			return new Hand() { Class = HandClass.Straight, CompareOrder = compareOrder };
		}

		// 07. ThreeOfAKind

		if (threeOfAKindRank != null)
		{
			compareOrder = new Card[cards.Length];

			var i = 0;

			foreach (Card card in cardsByRank[(Rank)threeOfAKindRank])
			{
				compareOrder[i++] = card;
			}

			foreach (Card card in cards)
			{
				if (card.Rank != threeOfAKindRank) compareOrder[i++] = card;
			}

			return new Hand() { Class = HandClass.ThreeOfAKind, CompareOrder = compareOrder };
		}

		// 08. TwoPair

		if (TryFindTwoPair(cards, cardsByRank, out compareOrder))
		{
			return new Hand() { Class = HandClass.TwoPair, CompareOrder = compareOrder };
		}

		// 09. Pair

		if (pairRank != null)
		{
			compareOrder = new Card[cards.Length];

			var i = 0;

			foreach (Card card in cardsByRank[(Rank)pairRank])
			{
				compareOrder[i++] = card;
			}

			foreach (Card card in cards)
			{
				if (card.Rank != pairRank) compareOrder[i++] = card;
			}

			return new Hand() { Class = HandClass.Pair, CompareOrder = compareOrder };
		}

		// 10. HighCard

		return new Hand() { Class = HandClass.HighCard, CompareOrder = cards };
	}

	private static bool TryFindRoyalFlush(Card[] cards, Dictionary<Suit, List<Card>> cardsBySuit,
		out bool isFlush)
	{
		isFlush = false;

		if (!TryFindFlush(cards, cardsBySuit)) return false;
		else isFlush = true;

		if (cards[0].Rank != Rank.Ace) return false;
		if (cards[1].Rank != Rank.King) return false;
		if (cards[2].Rank != Rank.Queen) return false;
		if (cards[3].Rank != Rank.Jack) return false;
		if (cards[4].Rank != Rank.Ten) return false;

		return true;
	}

	private static bool TryFindFourOfAKind(Card[] cards, Dictionary<Rank, List<Card>> cardsByRank,
		out Card[] compareOrder)
	{
		compareOrder = new Card[cards.Length];

		foreach (var rank in cardsByRank.Keys)
		{
			if (cardsByRank[rank].Count == 4)
			{
				int i = 0;
				foreach (Card card in cards)
				{
					if (card.Rank == rank)
					{
						compareOrder[i] = card;
						i++;
					}
				}
				compareOrder[^1] = cards.First(c => c.Rank != rank);
				return true;
			}
		}

		return false;
	}

	private static bool TryFindFullHouse(Card[] cards, Dictionary<Rank, List<Card>> cardsByRank, out Rank? threeOfAKindRank, out Rank? pairRank, out Card[] compareOrder)
	{
		compareOrder = new Card[cards.Length];

		threeOfAKindRank = null;
		pairRank = null;

		foreach (var rank in cardsByRank.Keys)
		{ 
			if (cardsByRank[rank].Count == 3)
			{
				threeOfAKindRank = rank;
				continue;
			}
			if (cardsByRank[rank].Count == 2)
			{
				pairRank = rank;
			}
		}

		if (pairRank == null) return false;
		if (threeOfAKindRank == null) return false;

		var i = 0;

		foreach(var card in cardsByRank[(Rank)threeOfAKindRank])
		{
			compareOrder[i++] = card;
		}

		foreach (var card in cardsByRank[(Rank)pairRank])
		{
			compareOrder[i++] = card;
		}

		return true;
	}

	private static bool TryFindFlush(Card[] cards, Dictionary<Suit, List<Card>> cardsBySuit)
	{
		foreach (var suit in cardsBySuit.Keys)
		{
			if (cardsBySuit[suit].Count == 5)
			{
				return true;
			}
		}

		return false;
	}

	private static bool TryFindStraight(Card[] cards, out Card[] compareOrder)
	{
		compareOrder = new Card[cards.Length];

		if (cards[0].Rank == Rank.Ace && cards[1].Rank == Rank.Five)
		{
			if (cards[2].Rank != Rank.Four) return false;
			if (cards[3].Rank != Rank.Three) return false;
			if (cards[4].Rank != Rank.Two) return false;
			compareOrder[0] = cards[1]; // Five
			compareOrder[1] = cards[2]; // Four
			compareOrder[2] = cards[3]; // Three
			compareOrder[3] = cards[4]; // Two
			compareOrder[4] = cards[0]; // Ace
			return true;
		}
		else
		{
			for (var i = 1; i < cards.Length; i++)
			{
				if(cards[0].CompareTo(cards[i]) != i) return false;
			}
		}

		Array.Copy(cards, compareOrder, cards.Length);
		return true;
	}

	private static bool TryFindTwoPair(Card[] cards, Dictionary<Rank, List<Card>> cardsByRank, out Card[] compareOrder)
	{
		compareOrder = new Card[cards.Length];

		Rank? pairRank1 = null;
		Rank? pairRank2 = null;

		foreach (var rank in cardsByRank.Keys)
		{
			if (cardsByRank[rank].Count == 2)
			{
				if(pairRank1 != null)
				{
					pairRank2 = rank;
					break;
				}
				pairRank1 = rank;
			}
		}

		if (pairRank1 == null || pairRank2 == null) return false;

		Rank higherPairRank = (Rank)(Math.Max((int)pairRank1, (int)pairRank2));
		Rank lowerPairRank  = (Rank)(Math.Min((int)pairRank1, (int)pairRank2));

		var i = 0;

		foreach (var card in cardsByRank[higherPairRank])
		{
			compareOrder[i++] = card;
		}

		foreach (var card in cardsByRank[lowerPairRank])
		{
			compareOrder[i++] = card;
		}

		// As 'cards' is sorted, the non-pair card is either first, third or fifth
		if      (cards[0].Rank != higherPairRank && cards[0].Rank != lowerPairRank) compareOrder[4] = cards[0];
		else if (cards[2].Rank != higherPairRank && cards[2].Rank != lowerPairRank) compareOrder[4] = cards[2];
		else if (cards[4].Rank != higherPairRank && cards[4].Rank != lowerPairRank) compareOrder[4] = cards[4];

		return true;
	}


	// Helpers

	private static Dictionary<Rank, List<Card>> GetCardsByRank(Card[] cards)
	{
		var map = new Dictionary<Rank, List<Card>>();

		foreach (var e in Enum.GetValues(typeof(Rank)))
		{
			map.Add((Rank)e, new List<Card>());
		}

		foreach (Card card in cards)
		{
			map[card.Rank].Add(card);
		}

		return map;
	}

	private static Dictionary<Suit, List<Card>> GetCardsBySuit(Card[] cards)
	{
		var map = new Dictionary<Suit, List<Card>>();

		foreach (var e in Enum.GetValues(typeof(Suit)))
		{
			map.Add((Suit)e, new List<Card>());
		}

		foreach (Card card in cards)
		{
			map[card.Suit].Add(card);
		}

		return map;
	}

	private static void SortDescending(Card[] cards)
	{
		Array.Sort(cards, (c1, c2) =>
		{
			int rankComparison = c1.CompareTo(c2);
			return rankComparison == 0 ? -c1.Suit.CompareTo(c2.Suit) : -rankComparison;
		});
	}
}
