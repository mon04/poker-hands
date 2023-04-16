using Model;
using PokerLogic;

namespace Test.HandDeterminerTests;

[TestFixture]
internal class FindingFourOfAKinds
{
    [Test]
    [TestCase("2h 2c 3d 2s 2d", "2s 2h 2d 2c 3d")]
    [TestCase("Ad As 6s Ah Ac", "As Ah Ad Ac 6s")]
    [TestCase("Jd Jh Qs Js Jc", "Js Jh Jd Jc Qs")]
    [TestCase("5c Tc 5h 5s 5d", "5s 5h 5d 5c Tc")]
    [TestCase("As Ks Kd Kh Kc", "Ks Kh Kd Kc As")]
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
