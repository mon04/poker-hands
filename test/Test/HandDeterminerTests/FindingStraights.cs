using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingStraights
{
    private const string Input1 = "2h 3h 6d 5h 4s";
    private const string Input2 = "9c 8d 7h 6h 5c";
    private const string Input3 = "Jc Ts 7c 9d 8c";
    private const string Input4 = "2c 3d 4h 5c Ad";
    private const string Input5 = "Ac Qd Kh Js Ts";

    private const string ExpCompareOrder1 = "6d 5h 4s 3h 2h";
    private const string ExpCompareOrder2 = "9c 8d 7h 6h 5c";
    private const string ExpCompareOrder3 = "Jc Ts 9d 8c 7c";
    private const string ExpCompareOrder4 = "5c 4h 3d 2c Ad";
    private const string ExpCompareOrder5 = "Ac Kh Qd Js Ts";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindStraight(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.Straight));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
