using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingFourOfAKinds
{
    private const string Input1 = "2h 2c 3d 2s 2d";
    private const string Input2 = "Ad As 6s Ah Ac";
    private const string Input3 = "Jd Jh Qs Js Jc";
    private const string Input4 = "5c Tc 5h 5s 5d";
    private const string Input5 = "As Ks Kd Kh Kc";

    private const string ExpCompareOrder1 = "2s 2h 2d 2c 3d";
    private const string ExpCompareOrder2 = "As Ah Ad Ac 6s";
    private const string ExpCompareOrder3 = "Js Jh Jd Jc Qs";
    private const string ExpCompareOrder4 = "5s 5h 5d 5c Tc";
    private const string ExpCompareOrder5 = "Ks Kh Kd Kc As";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindFourOfAKind(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.FourOfAKind));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
