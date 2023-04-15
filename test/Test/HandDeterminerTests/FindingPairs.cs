using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingPairs
{
    private const string Input1 =  "2h 2c 3d Jc Tc";
    private const string Input2 =  "Jd As 6s Ah Tc";
    private const string Input3 =  "As Ks Qs Js Jc";
    private const string Input4 =  "5c Tc 4h Qh 5d";
    private const string Input5 =  "As Ks Qs Js Kc";
    private const string Input6 =  "Qh Td 3d 8c Tc";
    private const string Input7 =  "Qh 8d 3d 8d Tc";
    private const string Input8 =  "3c 2h 8d 8c 9d";
    private const string Input9 =  "9c 2h 5d 7c 9d";
    private const string Input10 = "9c 2h Ad Ac 9d";

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
    public void FindPair(string handEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var hand = new Card[cardEncodings.Length];
        for(var i = 0; i < hand.Length; i++)
        {
            hand[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(hand, ((c1, c2) => - c1.CompareTo(c2)));

        Assert.That(HandDeterminer.TryFindPair(hand, out var rank, out var kickerRanks));

        Console.WriteLine(rank);
    }
}
