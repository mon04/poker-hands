using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingRoyalFlushes
{
    [Test]
    [TestCase("Ac Qc Kc Jc Tc", "Ac Kc Qc Jc Tc", "Split")]
    [TestCase("As Qs Ks Js Ts", "Ad Kd Qd Jd Td", "Split")]
    public void CompareRoyalFlushes(string hand1Encoded, string hand2Encoded, string expectedResult)
    {
        var cards1Encoded = hand1Encoded.Split(' ');
        var cards2Encoded = hand2Encoded.Split(' ');

        var cards1 = new Card[cards1Encoded.Length];
        var cards2 = new Card[cards2Encoded.Length];

        for (var i = 0; i < cards1.Length; i++)
        {
            cards1[i] = Card.FromEncoding(cards1Encoded[i]);
            cards2[i] = Card.FromEncoding(cards2Encoded[i]);
        }

        var hand1 = HandDeterminer.GetBestHand(cards1);
        var hand2 = HandDeterminer.GetBestHand(cards2);

        Assert.That(hand1.Class, Is.EqualTo(HandClass.RoyalFlush));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.RoyalFlush));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
