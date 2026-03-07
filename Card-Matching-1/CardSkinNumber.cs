using System;

class CardSkinNumber : ICardSkin
{
    static readonly string[] allShape = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

    string[] ICardSkin.allShape => allShape;

    public string GetDisplay(int cardValue)
    {
        if (cardValue == 0) return "*";
        return cardValue.ToString();
    }

    public ConsoleColor GetColor(int cardValue)
    {
        return ConsoleColor.White;
    }
}