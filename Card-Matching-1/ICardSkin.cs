using System;

interface ICardSkin
{
    string[] allShape { get; }

    string GetDisplay(int cardValue);
    ConsoleColor GetColor(int cardValue);
}