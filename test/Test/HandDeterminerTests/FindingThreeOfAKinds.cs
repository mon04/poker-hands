using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingThreeOfAKinds
{
    private const string Input1 = "2h 2c 3d Jc 2s";
    private const string Input2 = "Jd As Ac Ah Tc";
    private const string Input3 = "As Ks Jd Js Jc";
    private const string Input4 = "5c Tc 5h Qh 5d";
    private const string Input5 = "Kh Ks Qs Js Kc";
    private const string Input6 = "Qh Td 3d Th Tc";
    private const string Input7 = "2h 2c 3d 2d Tc";
    private const string Input8 = "3c 2h 8d 8c 8h";
    private const string Input9 = "9c 2h 5d 9h 9d";
    private const string Input10 = "7s 2h 7d Ac 7h";

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
    public void FindThreeOfAKind(string handEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var hand = new Card[cardEncodings.Length];
        for (var i = 0; i < hand.Length; i++)
        {
            hand[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(hand, ((c1, c2) => -c1.CompareTo(c2)));

        Assert.That(HandDeterminer.TryFindThreeOfAKind(hand, out var rank, out var kickerRanks));

        Console.WriteLine(rank);
    }
}
