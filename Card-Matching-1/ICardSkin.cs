using System;

interface ICardSkin
{
    string GetDisplay(int cardValue);
    ConsoleColor GetColor(int cardValue);
}