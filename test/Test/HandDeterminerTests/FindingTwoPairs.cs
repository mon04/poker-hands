using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingTwoPairs
{
    [Test]
    [TestCase("2h 2c 3d 3c Tc", "3d 3c 2h 2c Tc")]
    [TestCase("Jd As 6s Ah 6c", "As Ah 6s 6c Jd")]
    [TestCase("As Ad Qs Js Jc", "As Ad Js Jc Qs")]
    [TestCase("5c Tc 4h 4s 5d", "5d 5c 4s 4h Tc")]
    [TestCase("Jh Ks Qs Js Kc", "Ks Kc Js Jh Qs")]
    public void FindTwoPair(string handEncoded, string expCompareOrderEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var cards = new Card[cardEncodings.Length];
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i] = Card.FromEncoding(cardEncodings[i]);
        }

        var hand = HandDeterminer.GetBestHand(cards);

        var cardsEncoded = Helpers.CardsEncoded(cards);

        Console.WriteLine(hand.Class);
        Console.WriteLine(cardsEncoded);

        Assert.That(hand, Is.Not.Null);
        Assert.That(hand.Class, Is.EqualTo(HandClass.TwoPair));
        Assert.That(Helpers.CardsEncoded(hand.CompareOrder), Is.EqualTo(expCompareOrderEncoded));
    }
}
