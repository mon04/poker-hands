using Model;
using PokerLogic;

namespace Test.HandComparisonTests.SameClassComparisons;

[TestFixture]
internal class ComparingFourOfAKinds
{
    [Test]
    [TestCase("6s 6h 6d 6c Tc", "7s 7h 7d 7c 6c", "Hand 2")]
    [TestCase("5s As Ah Ad Ac", "Qs Qh Qd Qc 5s", "Hand 1")]
    [TestCase("Ts Th Td Tc 2d", "Ts Th Td Tc 9c", "Hand 2")]
    [TestCase("As Ah Ad Ac 2h", "As Ah Ad Ac 2h", "Split")]
    [TestCase("Ks Kh Kd Kc 8c", "Ks Kh 8d Kd Kc", "Split")]
    public void CompareFourOfAKinds(string hand1Encoded, string hand2Encoded, string expectedResult)
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

        Assert.That(hand1.Class, Is.EqualTo(HandClass.FourOfAKind));
        Assert.That(hand2.Class, Is.EqualTo(HandClass.FourOfAKind));

        var handComparison = hand1.CompareTo(hand2);

        string actualResult = handComparison > 0
            ? "Hand 1"
            : handComparison < 0
                ? "Hand 2"
                : "Split";

        Assert.That(actualResult, Is.EqualTo(expectedResult));
    }
}
