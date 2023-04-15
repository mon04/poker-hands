using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingTwoPairs
{
    private const string Input1 = "2h 2c 3d Jc Jd";
    private const string Input2 = "Jd As Js Ah Tc";
    private const string Input3 = "2s 2c Qs Kh Qh";
    private const string Input4 = "5c Tc 4h Th 5d";
    private const string Input5 = "As 2s 2c Js Jc";
    private const string Input6 = "Th Td 8d 8c 7c";
    private const string Input7 = "Kh 8d 3d Kd 3c";
    private const string Input8 = "3c 3h 8d 8c 9d";
    private const string Input9 = "Ac 2h 7d 7c Ad";
    private const string Input10 = "9c 9h 2d 2c Td";

    [Test]
    [TestCase(Input1)]
    [TestCase(Input2)]
    [TestCase(Input3)]
    [TestCase(Input4)]
    [TestCase(Input5)]
    [TestCase(Input6)]
    [TestCase(Input7)]
    [TestCase(Input8)]
    [TestCase(Input9)]
    [TestCase(Input10)]
    public void FindTwoPair(string handEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var hand = new Card[cardEncodings.Length];
        for (var i = 0; i < hand.Length; i++)
        {
            hand[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(hand, (c1, c2) => - c1.CompareTo(c2));

        foreach(var card in hand)
        {
            Console.WriteLine($"{card.Rank} of {card.Suit}");
        }

        Assert.That(HandDeterminer.TryFindTwoPair(hand, out var rank, out var kickerRanks));

        Console.WriteLine(rank);
    }
}
