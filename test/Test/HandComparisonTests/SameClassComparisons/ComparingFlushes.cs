using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingFlushes
{
    [Test]
    [TestCase("Ah 3h 6h Kh 4h", "3h 6h Kh 2h 8h", "Hand 1")]
    [TestCase("9c 2c Jc Qc 5c", "Kc 2c Jc Qc 5c", "Hand 2")]
    [TestCase("As 2s 5s 3s 6s", "Ks Js 9s Ts 7s", "Hand 1")]
    [TestCase("Jd 8d 7d 6d 4d", "Jc 8c 7c 6c 4c", "Split")]
    public void CompareFlushes(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.Flush));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.Flush));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
