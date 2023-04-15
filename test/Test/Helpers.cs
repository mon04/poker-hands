using Model;
using System.Text;

namespace Test;

internal static class Helpers
{
    internal static string CardsEncoded(Card[] cards)
    {
        var sb = new StringBuilder();
        foreach (var card in cards)
        {
            sb.Append(card.Encoding);
            sb.Append(' ');
        }
        return sb.ToString().Trim();
    }
}
