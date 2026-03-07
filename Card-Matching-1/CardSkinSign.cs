using System;

class CardSkinSign : ICardSkin
{
    Array values = Enum.GetValues(typeof(ConsoleColor));
    public static readonly string[] allShape = { "★", "♠", "♥", "◆", "♣", "●", "■", "▲", "▼", "◎", "◈", "▣" };

    string[] ICardSkin.allShape => allShape;

    public string GetDisplay(int cardValue)
    {
        if (cardValue == 0) return "*";
        return allShape[cardValue - 1].ToString();
    }

    public ConsoleColor GetColor(int cardValue)
    {
        if (cardValue == 0) return ConsoleColor.White;
        return (ConsoleColor)values.GetValue(cardValue);
    }
}