using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingStraightFlushes
{
    private const string Input1 = "2h 3h 6h 5h 4h";
    private const string Input2 = "9c 8c 7c 6c 5c";
    private const string Input3 = "Jd Td 7d 9d 8d";
    private const string Input4 = "2s 3s 4s 5s As";
    private const string Input5 = "9d Qd Kd Jd Td";

    private const string ExpCompareOrder1 = "6h 5h 4h 3h 2h";
    private const string ExpCompareOrder2 = "9c 8c 7c 6c 5c";
    private const string ExpCompareOrder3 = "Jd Td 9d 8d 7d";
    private const string ExpCompareOrder4 = "5s 4s 3s 2s As";
    private const string ExpCompareOrder5 = "Kd Qd Jd Td 9d";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindStraightFlush(string handEncoded, string expCompareOrderEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var cards = new Card[cardEncodings.Length];
        for(var i = 0; i < cards.Length; i++)
        {
            cards[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(cards, ((c1, c2) => - c1.CompareTo(c2)));

        var hand = HandDeterminer.GetHand(cards);

        var cardsEncoded = Helpers.CardsEncoded(cards);

        Console.WriteLine(hand.Class);
        Console.WriteLine(cardsEncoded);

        Assert.That(hand, Is.Not.Null);
        Assert.That(hand.Class, Is.EqualTo(HandClass.StraightFlush));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
