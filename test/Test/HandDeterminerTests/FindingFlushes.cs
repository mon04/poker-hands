using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingFlushes
{
    private const string Input1 = "2h 3h 6h Qh Ah";
    private const string Input2 = "Jd 2d Td 7d 9d";
    private const string Input3 = "Ac Tc 7c 5c 3c";
    private const string Input4 = "9s 3s 7s Js Qs";
    private const string Input5 = "As Ts 9s 8s 7s";

    private const string ExpCompareOrder1 = "Ah Qh 6h 3h 2h";
    private const string ExpCompareOrder2 = "Jd Td 9d 7d 2d";
    private const string ExpCompareOrder3 = "Ac Tc 7c 5c 3c";
    private const string ExpCompareOrder4 = "Qs Js 9s 7s 3s";
    private const string ExpCompareOrder5 = "As Ts 9s 8s 7s";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindFlush(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.Flush));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
