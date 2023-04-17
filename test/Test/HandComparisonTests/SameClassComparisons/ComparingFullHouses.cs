using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingFullHouses
{
    [Test]
    [TestCase("8d 8c 9c 9d 9h", "9d 9c Js Jc Jd", "Hand 2")]
    [TestCase("Jd Jc 6h 6d Js", "2h 2c Ks Kd Kh", "Hand 2")]
    [TestCase("8d 8c 7h 7c 7d", "7s 7h 7h 2d 2c", "Hand 1")]
    [TestCase("Kh Kd 5s 5d Kc", "Ks Kc 5c 5h 5s", "Hand 1")]
    [TestCase("As Ah 4d 4s 4c", "As Ah 4d 4s 4c", "Split")]
    public void CompareFullHouse(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.FullHouse));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.FullHouse));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
