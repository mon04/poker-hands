using Model;
using PokerLogic;

namespace PokerGameCLI;

public class Program
{
	public static void Main(String[] args)
	{
		var deck = DeckLogic.GetDeck();
		deck = DeckLogic.Shuffle(deck);

		var playersPockets = new Card[5, 2];
		for (int i = 0; i < playersPockets.GetLength(0); i++)
		{
			for (int j = 0; j < playersPockets.GetLength(1); j++)
			{
				playersPockets[i, j] = deck.Dequeue();
			}
		}

		deck.Dequeue();
		var flopCards = new Card[3];
		flopCards[0] = deck.Dequeue();
		flopCards[1] = deck.Dequeue();
		flopCards[2] = deck.Dequeue();

		Hand[] playersHands = new Hand[5];

		for(int i = 0; i < playersHands.Length; i++)
		{
			var playersCards = new Card[5];

			var j = 0;
			foreach(Card card in flopCards)
			{
				playersCards[j++] = card;
			}
			playersCards[j++] = playersPockets[i, 0];
			playersCards[j++] = playersPockets[i, 1];

			playersHands[i] = HandDeterminer.GetBestHand(playersCards);
			Console.WriteLine($"Player[{i}] has hand: [{playersHands[i]}] {playersHands[i].Class}");
		}
	}
}
