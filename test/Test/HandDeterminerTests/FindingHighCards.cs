using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingHighCards
{
    private const string Input1 =  "As Qh 2c 7h Td";
    private const string Input2 =  "Qd 7c 3d 2d Th";
    private const string Input3 =  "9h Td Jc Qs 2h";
    private const string Input4 =  "7h 2h 3h 9h Ac";
    private const string Input5 =  "6d 7s Ah Kd 8c";

    private const string ExpCompareOrder1 = "As Qh Td 7h 2c";
    private const string ExpCompareOrder2 = "Qd Th 7c 3d 2d";
    private const string ExpCompareOrder3 = "Qs Jc Td 9h 2h";
    private const string ExpCompareOrder4 = "Ac 9h 7h 3h 2h";
    private const string ExpCompareOrder5 = "Ah Kd 8c 7s 6d";

    [Test]
    [TestCase(Input1, ExpCompareOrder1)]
    [TestCase(Input2, ExpCompareOrder2)]
    [TestCase(Input3, ExpCompareOrder3)]
    [TestCase(Input4, ExpCompareOrder4)]
    [TestCase(Input5, ExpCompareOrder5)]
    public void FindHighCard(string handEncoded, string expCompareOrderEncoded)
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
        Assert.That(hand.Class, Is.EqualTo(HandClass.HighCard));
        Assert.That(Helpers.CardsEncoded(cards), Is.EqualTo(expCompareOrderEncoded));
    }
}
