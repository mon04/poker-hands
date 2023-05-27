using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingTwoPairs
{
    [Test]
    [TestCase("8d 8c 9c 6h 9h", "9d 9c As Jc Jd", "Hand 2")]
    [TestCase("Js Jc 6h 6d 2s", "2h 2c Js Kd Kh", "Hand 2")]
    [TestCase("8d 8c 7h 3c 3d", "8s 8h 6h 2d 2c", "Hand 1")]
    [TestCase("Kh Kd 5s 5d 2h", "Ks Kc 5c 5h 4h", "Hand 2")]
    [TestCase("As Ah 5d 4c 4c", "As Ah 5d 4c 4c", "Split")]
    public void CompareTwoPairs(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.TwoPair));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.TwoPair));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
