using Model;
using PokerLogic;

namespace PokerGameCLI;

public class Program
{
	private static readonly int PlayerCount = 8;

	public static void Main(String[] args)
	{
		var deck = DeckLogic.GetDeck();
		deck = DeckLogic.Shuffle(deck);

		var playersPockets = new Card[PlayerCount, 2];
		for (int i = 0; i < playersPockets.GetLength(0); i++)
		{
			for (int j = 0; j < playersPockets.GetLength(1); j++)
			{
				playersPockets[i, j] = deck.Dequeue();
			}
		}

		var communityCards = new Card[5];
		deck.Dequeue();
		communityCards[0] = deck.Dequeue();
		communityCards[1] = deck.Dequeue();
		communityCards[2] = deck.Dequeue();
		deck.Dequeue();
		communityCards[3] = deck.Dequeue();
		deck.Dequeue();
		communityCards[4] = deck.Dequeue();

		Console.WriteLine("Board cards: [{0}]", string.Join(", ", communityCards.Select(x => x.Encoding)));

		Hand[] playersHands = new Hand[playersPockets.GetLength(0)];

		for(int i = 0; i < playersHands.Length; i++)
		{
			var playersCards = new Card[7];

			var j = 0;
			foreach(Card card in communityCards)
			{
				playersCards[j++] = card;
			}
			playersCards[j++] = playersPockets[i, 0];
			playersCards[j++] = playersPockets[i, 1];

			playersHands[i] = HandDeterminer.GetBestHand(playersCards);
		}

		var winners = new List<int>();

		for(int i=0; i < playersHands.Length; i++)
		{
			if(winners.Count == 0)
			{
				winners.Add(i);
				continue;
			}

			int comparison = playersHands[i].CompareTo(playersHands[winners[0]]);
			if (comparison > 0)
			{
				winners.Clear();
				winners.Add(i);
			}
			else if(comparison == 0)
			{
				winners.Add(i);
			}
		}

		for(int i=0; i < playersPockets.GetLength(0); i++)
		{
			Console.WriteLine($"Player[{i}] pocket: [{playersPockets[i,0].Encoding}, {playersPockets[i, 1].Encoding}]");
		}

		for (int i = 0; i < playersHands.Length; i++)
		{
			if (winners.Contains(i)) Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Player[{i}] hand: [{playersHands[i]}] {playersHands[i].Class}");
			Console.ResetColor();
		}

		foreach (var winner in winners)
		{
			Console.WriteLine($"Player[{winner}] wins!");
		}
	}
}
