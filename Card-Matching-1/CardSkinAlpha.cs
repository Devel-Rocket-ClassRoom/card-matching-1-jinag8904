using System;

class CardSkinAlpha : ICardSkin
{
    Array values = Enum.GetValues(typeof(ConsoleColor));
    public static readonly string[] allShape = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };    

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