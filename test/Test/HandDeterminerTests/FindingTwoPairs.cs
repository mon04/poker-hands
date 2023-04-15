using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingTwoPairs
{
    private const string Input1 = "2h 2c 3d 3c Tc";
    private const string Input2 = "Jd As 6s Ah 6c";
    private const string Input3 = "As Ad Qs Js Jc";
    private const string Input4 = "5c Tc 4h 4s 5d";
    private const string Input5 = "Jh Ks Qs Js Kc";
    private const string Input6 = "Qh Td 3d 3c Tc";
    private const string Input7 = "Qh 8c Td 8d Tc";
    private const string Input8 = "3c 2h 8d 8c 3d";
    private const string Input9 = "9c 2h 2d 7c 9d";
    private const string Input10 = "9c 2h Ad Ac 2d";

    private const string ExpCompareOrder1 = "3d 3c 2h 2c Tc";
    private const string ExpCompareOrder2 = "As Ah 6s 6c Jd";
    private const string ExpCompareOrder3 = "As Ad Js Jc Qs";
    private const string ExpCompareOrder4 = "5d 5c 4s 4h Tc";
    private const string ExpCompareOrder5 = "Ks Kc Js Jh Qs";
    private const string ExpCompareOrder6 = "Td Tc 3d 3c Qh";
    private const string ExpCompareOrder7 = "Td Tc 8d 8c Qh";
    private const string ExpCompareOrder8 = "8d 8c 3d 3c 2h";
    private const string ExpCompareOrder9 = "9d 9c 2h 2d 7c";
    private const string ExpCompareOrder10 = "Ad Ac 2h 2d 9c";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    [TestCase(Input6, ExpCompareOrder6)]
    [TestCase(Input7, ExpCompareOrder7)]
    [TestCase(Input8, ExpCompareOrder8)]
    [TestCase(Input9, ExpCompareOrder9)]
    [TestCase(Input10, ExpCompareOrder10)]
    public void FindTwoPair(string handEncoded, string expCompareOrderEncoded)
    {
        var cardEncodings = handEncoded.Split(' ');
        var cards = new Card[cardEncodings.Length];
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i] = Card.FromEncoding(cardEncodings[i]);
        }
        Array.Sort(cards, ((c1, c2) => -c1.CompareTo(c2)));

        var hand = HandDeterminer.GetHand(cards);

        var cardsEncoded = Helpers.CardsEncoded(cards);

        Console.WriteLine(hand.Class);
        Console.WriteLine(cardsEncoded);

        Assert.That(hand, Is.Not.Null);
        Assert.That(hand.Class, Is.EqualTo(HandClass.TwoPair));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
