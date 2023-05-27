using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingThreeOfAKinds
{
    [Test]
    [TestCase("8h 8d 8c 9c 6h", "9s 9d 9c 3s 2c", "Hand 2")]
    [TestCase("Js Jd Jc 6d 2s", "2h 2d 2c Td 3h", "Hand 1")]
    [TestCase("8s 8d 8c 4d 2c", "8s 8h 8c 3c 2d", "Hand 1")]
    [TestCase("Kh Kd Kc 5d 2h", "Ks Kd Kc 5d 4h", "Hand 2")]
    [TestCase("As Ah Ad 4c 2c", "As Ah Ad 4c 2c", "Split")]
    [TestCase("Kh Kd Kc 5d 2h", "Ks Kc Ks 5d 2h", "Split")]
    public void CompareThreeOfAKinds(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.ThreeOfAKind));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.ThreeOfAKind));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
