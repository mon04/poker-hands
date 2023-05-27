using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingFlushes
{
    [Test]
    [TestCase("2h 3h 6h Qh Ah", "Ah Qh 6h 3h 2h")]
    [TestCase("Jd 2d Td 7d 9d", "Jd Td 9d 7d 2d")]
    [TestCase("Ac Tc 7c 5c 3c", "Ac Tc 7c 5c 3c")]
    [TestCase("9s 3s 7s Js Qs", "Qs Js 9s 7s 3s")]
    [TestCase("As Ts 9s 8s 7s", "As Ts 9s 8s 7s")]
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.Flush));
        Assert.That(Helpers.CardsEncoded(hand.CompareOrder), Is.EqualTo(expCompareOrderEncoded));
    }
}
