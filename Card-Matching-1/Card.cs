
using System;

class Card
{
    Random rand = new Random();

    static int[] allCards = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
    int[,] mixedCards = new int[4, 4];
    string[,] revealedCards = new string[4, 4];

    bool[] randomedCards = new bool[16];
    int selected;

    int tryCount;
    public int foundCount;

    string input;
    int haeng1, yeol1;
    int haeng2, yeol2;

    public void CardMix()   // 카드 섞기 메서드
    {
        revealedCards = new string[4, 4];   // 필드 초기화
        mixedCards = new int[4, 4];
        randomedCards = new bool[16];

        tryCount = 0;
        foundCount = 0;

        Console.WriteLine("카드를 섞는 중...");

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                revealedCards[i, j] = "**";

                while (true)
                {
                    selected = rand.Next(16);
                    if (randomedCards[selected] == true) continue;

                    mixedCards[i, j] = allCards[selected];
                    randomedCards[selected] = true;
                    break;
                }                
            }
        }
    }

    public bool IsSameCard(int index11, int index12, int index21, int index22)
    {
        return mixedCards[index11, index12] == mixedCards[index21, index22];
    }

    public void ShowCards()
    {
        Console.WriteLine("\n    1열 2열 3열 4열");

        for (int i = 0; i < 4; i++)
        {
            Console.Write($"{i+1}행 ");

            for (int j = 0; j < 4; j++)
            {
                Console.Write($"{revealedCards[i, j]}  ");
            }

            Console.WriteLine();
        }
    }

    public void ShowCount()
    {
        Console.WriteLine($"\n시도 횟수: {tryCount} | 찾은 쌍: {foundCount}/8\n");
    }

    public void SelectCards()
    {
        Console.Write("첫 번째 카드를 선택하세요 (행 열): ");
        input = Console.ReadLine();
        haeng1 = int.Parse(input.Split(' ')[0]) -1;
        yeol1 = int.Parse(input.Split(' ')[1]) -1;

        revealedCards[haeng1, yeol1] = $"[{mixedCards[haeng1, yeol1].ToString()}]";
        ShowCards();

        Console.Write("\n두 번째 카드를 선택하세요 (행 열): ");
        input = Console.ReadLine();
        haeng2 = int.Parse(input.Split(' ')[0]) -1;
        yeol2 = int.Parse(input.Split(' ')[1]) -1;

        revealedCards[haeng2, yeol2] = $"[{mixedCards[haeng2, yeol2].ToString()}]";
        ShowCards();

        if (mixedCards[haeng1, yeol1] != mixedCards[haeng2, yeol2])
        {
            Console.WriteLine("\n짝이 맞지 않습니다!");

            revealedCards[haeng1, yeol1] = "**";
            revealedCards[haeng2, yeol2] = "**";
        }

        else
        {
            Console.WriteLine("\n짝을 찾았습니다!");

            revealedCards[haeng1, yeol1] = mixedCards[haeng1, yeol1].ToString();
            revealedCards[haeng2, yeol2] = mixedCards[haeng2, yeol2].ToString();

            foundCount++;
        }

        tryCount++;
    }

    public bool GameEnd()
    {
        Console.WriteLine("=== 게임 클리어! ===");
        Console.WriteLine($"총 시도 횟수: {tryCount}\n");

        while (true)
        {
            Console.Write("새 게임을 하시겠습니까? (Y/N): ");
            input = Console.ReadLine();

            if (input == "n" || input == "N")
            {
                Console.WriteLine("게임을 종료합니다.");
                return false;
            }

            else if (input == "y" || input == "Y")
            {
                Console.Clear();
                return true;
            }

            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

enum Level
{
    Easy,
    Normal,
    Hard
}