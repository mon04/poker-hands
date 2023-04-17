using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingStraightFlushes
{
    [Test]
    [TestCase("2h 3h 6h 5h 4h", "3h 6h 5h 4h 7h", "Hand 2")]
    [TestCase("9c 8c 7c 6c 5c", "Qh Jh Th 8h 9h", "Hand 2")]
    [TestCase("Jd Td 7d 9d 8d", "Td 7d 9d 8d 6d", "Hand 1")]
    [TestCase("2s 3s 4s 5s As", "6s 5s 4s 3s 2s", "Hand 2")]
    [TestCase("9c Qc Kc Jc Tc", "9c Kc Qc Jc Tc", "Split")]
    [TestCase("9s Qs Ks Js Ts", "9d Kd Qd Jd Td", "Split")]
    public void CompareStraightFlushes(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        var hand1 = HandDeterminer.GetHand(cards1);
        var hand2 = HandDeterminer.GetHand(cards2);

        Assert.That(hand1.Class, Is.EqualTo(HandClass.StraightFlush));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.StraightFlush));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
