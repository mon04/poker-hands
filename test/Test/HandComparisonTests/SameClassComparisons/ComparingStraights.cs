using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingStraights
{
    [Test]
    [TestCase("2h 3h 6d 5h 4s", "3h 6d 5h 4s 7c", "Hand 2")]
    [TestCase("9c 8d 7h 6h 5c", "Qh Jh Tc 8d 9h", "Hand 2")]
    [TestCase("Jc Ts 7c 9d 8c", "Ts 7c 9d 8c 6h", "Hand 1")]
    [TestCase("2c 3d 4h 5c Ad", "6c 5c 4h 3d 2c", "Hand 2")]
    [TestCase("Ac Qd Kh Js Ts", "Ac Kh Qd Js Ts", "Split")]
    [TestCase("Ac Qd Kh Js Ts", "Ah Kh Qd Js Tc", "Split")]
    public void CompareStraights(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.Straight));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.Straight));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
