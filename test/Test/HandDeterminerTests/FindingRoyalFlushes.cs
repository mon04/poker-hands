using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingRoyalFlushes
{
    [Test]
    [TestCase("As Ks Qs Js Ts", "As Ks Qs Js Ts")]
    [TestCase("Th Jh Qh Kh Ah", "Ah Kh Qh Jh Th")]
    [TestCase("Td Kd Qd Jd Ad", "Ad Kd Qd Jd Td")]
    [TestCase("Ac Kc Tc Qc Jc", "Ac Kc Qc Jc Tc")]
    public void FindFlush(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.RoyalFlush));
        Assert.That(Helpers.CardsEncoded(hand.CompareOrder), Is.EqualTo(expCompareOrderEncoded));
    }
}
