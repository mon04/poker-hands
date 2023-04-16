using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingPairs
{
    [Test]
    [TestCase("2h 2c 3d Jc Tc", "2h 2c Jc Tc 3d")]
    [TestCase("Jd As 6s Ah Tc", "As Ah Jd Tc 6s")]
    [TestCase("As Ks Qs Js Jc", "Js Jc As Ks Qs")]
    [TestCase("5c Tc 4h Qh 5d", "5d 5c Qh Tc 4h")]
    [TestCase("As Ks Qs Js Kc", "Ks Kc As Qs Js")]
    public void FindPair(string handEncoded, string expCompareOrderEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var cards = new Card[cardEncodings.Length];
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(cards, ((c1, c2) => -c1.CompareTo(c2)));

        var hand = HandDeterminer.GetHand(cards);

        var cardsEncoded = Helpers.CardsEncoded(cards);

        Console.WriteLine(hand.Class);
        Console.WriteLine(cardsEncoded);

        Assert.That(hand, Is.Not.Null);
        Assert.That(hand.Class, Is.EqualTo(HandClass.Pair));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
