using System;

class CardSkinNumber : ICardSkin
{
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