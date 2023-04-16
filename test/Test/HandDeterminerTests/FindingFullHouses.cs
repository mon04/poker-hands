using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingFullHouses
{
    [Test]
    [TestCase("2h 2c 3d 3c 2d", "2h 2d 2c 3d 3c")]
    [TestCase("Ac As 6s Ah 6c", "As Ah Ac 6s 6c")]
    [TestCase("As Ad Jd Js Jc", "Js Jd Jc As Ad")]
    [TestCase("5d 5c 4s 4h 5h", "5h 5d 5c 4s 4h")]
    [TestCase("Jh Ks Jd Js Kc", "Js Jh Jd Ks Kc")]
    public void FindFullHouse(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.FullHouse));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
