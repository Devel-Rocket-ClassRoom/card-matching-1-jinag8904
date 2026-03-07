using System;
using System.Threading;

class Cards
{
    static readonly int[] allCards = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };

    ICardSkin cardSkin;
    static string[] actualShape;

    int[,] mixedCards;
    public int[,] revealedCards;

    Random rand = new Random();
    bool[] randomedCards;
    int selected;

    public Cards(Level level, Skin skin, out int numberOfCards) // 난이도 별 카드 개수와 스킨 설정
    {
        switch (level)
        {
            case Level.Easy:
                revealedCards = new int[2, 4];
                mixedCards = new int[2, 4];
                randomedCards = new bool[8];
                numberOfCards = 8;
                break;

            case Level.Normal:
                revealedCards = new int[4, 4];
                mixedCards = new int[4, 4];
                randomedCards = new bool[16];
                numberOfCards = 16;
                break;

            case Level.Hard:
                revealedCards = new int[4, 6];
                mixedCards = new int[4, 6];
                randomedCards = new bool[24];
                numberOfCards = 24;
                break;

            default:
                numberOfCards = 0;
                break;
        }

        switch (skin)
        {
            case Skin.Number:
                cardSkin = new CardSkinNumber();
                actualShape = cardSkin.allShape;
                break;

            case Skin.Alphabet:
                cardSkin = new CardSkinAlpha();
                actualShape = cardSkin.allShape;
                break;

            case Skin.Sign:
                cardSkin = new CardSkinSign();
                actualShape = cardSkin.allShape;
                break;
        }
    }

    public void Mix()
    {
        Console.Clear();
        Console.WriteLine("카드를 섞는 중...");

        Thread.Sleep(2000);

        for (int i = 0; i < revealedCards.GetLength(0); i++)
        {
            for (int j = 0; j < revealedCards.GetLength(1); j++)
            {
                revealedCards[i, j] = 0;

                while (true)
                {
                    selected = rand.Next(randomedCards.Length);
                    if (randomedCards[selected] == true) continue;

                    mixedCards[i, j] = allCards[selected];

                    randomedCards[selected] = true;
                    break;
                }
            }
        }
    }

    public void RevealCard(int row, int column)
    {
        revealedCards[row, column] = mixedCards[row, column];
    }

    public void HideCard(int row, int column)
    {
        revealedCards[row, column] = 0;
    }

    public void RevealAll()
    {
        for (int i = 0; i < mixedCards.GetLength(0); i++)
        {
            for (int j = 0; j < mixedCards.GetLength(1); j++)
            {
                revealedCards[i, j] = mixedCards[i, j];
            }
        }
    }

    public void HideAll()
    {
        for (int i = 0; i < mixedCards.GetLength(0); i++)
        {
            for (int j = 0; j < mixedCards.GetLength(1); j++)
            {
                revealedCards[i, j] = 0;
            }
        }
    }

    public void ShowRevealedCards(Skin skin)
    {
        Console.Write("    ");

        for (int i = 0; i < revealedCards.GetLength(1); i++)
        {
            Console.Write($"{i + 1}열 ");
        }

        Console.WriteLine();

        for (int i = 0; i < revealedCards.GetLength(0); i++)
        {
            Console.Write($"{i + 1}행 ");

            for (int j = 0; j < revealedCards.GetLength(1); j++)
            {
                Console.ForegroundColor = cardSkin.GetColor(revealedCards[i, j]);
                Console.Write($" {cardSkin.GetDisplay(revealedCards[i, j])}  ");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    public bool IsOutOfRangeOrAlreadyRevealed(int row, int column)
    {
        if (row >= 0 && row < mixedCards.GetLength(0) && column >= 0 && column < mixedCards.GetLength(1) && revealedCards[row, column] == 0)
            return false;

        return true;
    }

    public bool IsMatched(int row1, int column1, int row2, int column2)
    {
        if (mixedCards[row1, column1] != mixedCards[row2, column2])
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}