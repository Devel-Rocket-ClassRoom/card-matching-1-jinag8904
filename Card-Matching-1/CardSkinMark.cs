using System;

class CardSkinMark : ICardSkin
{
    public static readonly string[] allShape = { "★", "♠", "♥", "◆", "♣", "●", "■", "▲", "▼", "◎", "◈", "▣" };

    private readonly ConsoleColor[] cardColors = {
        ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Red, ConsoleColor.Green,
        ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.DarkYellow,
        ConsoleColor.DarkBlue, ConsoleColor.DarkRed, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan
    };

    string[] ICardSkin.allShape => allShape;

    public string GetDisplay(int cardValue)
    {
        if (cardValue == 0) return "   **";
        return $"{allShape[cardValue - 1].ToString(), 4}";
    }

    public ConsoleColor GetColor(int cardValue)
    {
        if (cardValue == 0) return ConsoleColor.White;
        return cardColors[cardValue - 1];
    }
}