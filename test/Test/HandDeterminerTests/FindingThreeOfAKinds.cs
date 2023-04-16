using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingThreeOfAKinds
{
    private const string Input1 = "2h 2c 3d Jc 2d";
    private const string Input2 = "Ad As 6s Ah Tc";
    private const string Input3 = "As Jh Qs Js Jc";
    private const string Input4 = "5c Tc 5h Qh 5d";
    private const string Input5 = "As Ks Qs Kh Kc";

    private const string ExpCompareOrder1 = "2h 2d 2c Jc 3d";
    private const string ExpCompareOrder2 = "As Ah Ad Tc 6s";
    private const string ExpCompareOrder3 = "Js Jh Jc As Qs";
    private const string ExpCompareOrder4 = "5h 5d 5c Qh Tc";
    private const string ExpCompareOrder5 = "Ks Kh Kc As Qs";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindThreeOfAKind(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.ThreeOfAKind));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
