using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingRoyalFlushes
{
    private const string Input1 = "As Ks Qs Js Ts";
    private const string Input2 = "Th Jh Qh Kh Ah";
    private const string Input3 = "Td Kd Qd Jd Ad";
    private const string Input4 = "Ac Kc Tc Qc Jc";

    private const string ExpCompareOrder1 = "As Ks Qs Js Ts";
    private const string ExpCompareOrder2 = "Ah Kh Qh Jh Th";
    private const string ExpCompareOrder3 = "Ad Kd Qd Jd Td";
    private const string ExpCompareOrder4 = "Ac Kc Qc Jc Tc";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.RoyalFlush));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
