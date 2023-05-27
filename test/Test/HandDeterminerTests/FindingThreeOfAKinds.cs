using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingThreeOfAKinds
{
    [Test]
    [TestCase("2h 2c 3d Jc 2d", "2h 2d 2c Jc 3d")]
    [TestCase("Ad As 6s Ah Tc", "As Ah Ad Tc 6s")]
    [TestCase("As Jh Qs Js Jc", "Js Jh Jc As Qs")]
    [TestCase("5c Tc 5h Qh 5d", "5h 5d 5c Qh Tc")]
    [TestCase("As Ks Qs Kh Kc", "Ks Kh Kc As Qs")]
    public void FindThreeOfAKind(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.ThreeOfAKind));
        Assert.That(Helpers.CardsEncoded(hand.CompareOrder), Is.EqualTo(expCompareOrderEncoded));
    }
}
