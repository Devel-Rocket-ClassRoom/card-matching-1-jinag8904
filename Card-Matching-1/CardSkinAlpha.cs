using System;

class CardSkinAlpha : ICardSkin
{
    Array values = Enum.GetValues(typeof(ConsoleColor));
    static string[] allCardsAlpha = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };

    public string GetDisplay(int cardValue)
    {
        if (cardValue == 0) return "*";
        return allCardsAlpha[cardValue - 1].ToString();
    }

    public ConsoleColor GetColor(int cardValue)
    {
        if (cardValue == 0) return ConsoleColor.White;
        return (ConsoleColor)values.GetValue(cardValue);
    }
}