using System;

class CardSkinSign : ICardSkin
{
    Array values = Enum.GetValues(typeof(ConsoleColor));
    static string[] allCardsSign = { "★", "♠", "♥", "◆", "♣", "●", "■", "▲", "▼", "◎", "◈", "▣" };

    public string GetDisplay(int cardValue)
    {
        if (cardValue == 0) return "*";
        return allCardsSign[cardValue - 1].ToString();
    }

    public ConsoleColor GetColor(int cardValue)
    {
        if (cardValue == 0) return ConsoleColor.White;
        return (ConsoleColor)values.GetValue(cardValue);
    }
}