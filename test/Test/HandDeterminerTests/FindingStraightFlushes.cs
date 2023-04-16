using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingStraightFlushes
{
    [Test]
    [TestCase("2h 3h 6h 5h 4h", "6h 5h 4h 3h 2h")]
    [TestCase("9c 8c 7c 6c 5c", "9c 8c 7c 6c 5c")]
    [TestCase("Jd Td 7d 9d 8d", "Jd Td 9d 8d 7d")]
    [TestCase("2s 3s 4s 5s As", "5s 4s 3s 2s As")]
    [TestCase("9d Qd Kd Jd Td", "Kd Qd Jd Td 9d")]
    public void FindStraightFlush(string handEncoded, string expCompareOrderEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var cards = new Card[cardEncodings.Length];
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i] = Card.FromEncoding(cardEncodings[i]);
        }

        var hand = HandDeterminer.GetHand(cards);

        var cardsEncoded = Helpers.CardsEncoded(cards);

        Console.WriteLine(hand.Class);
        Console.WriteLine(cardsEncoded);

        Assert.That(hand, Is.Not.Null);
        Assert.That(hand.Class, Is.EqualTo(HandClass.StraightFlush));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
