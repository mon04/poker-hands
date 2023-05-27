using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingHighCards
{
    [Test]
    [TestCase("8h 7h 5d 3c 2s", "7s 6h 5d 4c 2c", "Hand 1")]
    [TestCase("Js Ts 6h 5c 2c", "Js Ts 6h 5c 3c", "Hand 2")]
    [TestCase("As Kc Qh 8s 7c", "Ah Kc Qh 7c 5c", "Hand 1")]
    [TestCase("Kh Qh Jh 8s 7c", "Kh Qh Jh 8s 7c", "Split")]
    [TestCase("As Kh Jd 4c 2c", "Ac Ks Js 4h 2h", "Split")]
    public void CompareHighCards(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.HighCard));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.HighCard));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
