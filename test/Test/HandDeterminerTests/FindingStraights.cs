using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingStraights
{
    [Test]
    [TestCase("2h 3h 6d 5h 4s", "6d 5h 4s 3h 2h")]
    [TestCase("9c 8d 7h 6h 5c", "9c 8d 7h 6h 5c")]
    [TestCase("Jc Ts 7c 9d 8c", "Jc Ts 9d 8c 7c")]
    [TestCase("2c 3d 4h 5c Ad", "5c 4h 3d 2c Ad")]
    [TestCase("Ac Qd Kh Js Ts", "Ac Kh Qd Js Ts")]
    public void FindStraight(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.Straight));
        Assert.That(Helpers.CardsEncoded(hand.CompareOrder), Is.EqualTo(expCompareOrderEncoded));
    }
}
