﻿using Model;
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

    private const string ExpCompareOrder1 = "3d 3c 2h 2c Tc";
    private const string ExpCompareOrder2 = "As Ah 6s 6c Jd";
    private const string ExpCompareOrder3 = "As Ad Js Jc Qs";
    private const string ExpCompareOrder4 = "5d 5c 4s 4h Tc";
    private const string ExpCompareOrder5 = "Ks Kc Js Jh Qs";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
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
