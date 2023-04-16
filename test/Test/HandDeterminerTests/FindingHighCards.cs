using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingHighCards
{
    [Test]
    [TestCase("As Qh 2c 7h Td", "As Qh Td 7h 2c")]
    [TestCase("Qd 7c 3d 2d Th", "Qd Th 7c 3d 2d")]
    [TestCase("9h Td Jc Qs 2h", "Qs Jc Td 9h 2h")]
    [TestCase("7h 2h 3h 9h Ac", "Ac 9h 7h 3h 2h")]
    [TestCase("6d 7s Ah Kd 8c", "Ah Kd 8c 7s 6d")]
    public void FindHighCard(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.HighCard));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
