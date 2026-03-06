
using System;

class Card
{
    Random rand = new Random();
    Level level;
    public Result gameResult = Result.Undecided;

    static int[] allCards = { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12, 12 };
    int[,] mixedCards;
    string[,] revealedCards;

    bool[] randomedCards;
    int selected;

    public int tryCount;
    public int foundCount;
    public int haveToFoundCount;
    public int tryLimit;

    string input;
    int haeng1, yeol1;
    int haeng2, yeol2;

    public void LevelSetting()
    {
        while (true)
        {
            Console.WriteLine("\n난이도를 선택하세요:\n1. 쉬움 (2x4)\n2. 보통 (4x4)\n3. 어려움 (4x6)");
            Console.Write("선택: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    level = Level.Easy;
                    return;
                case "2":
                    level = Level.Normal;
                    return;
                case "3":
                    level = Level.Hard;
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }
    }

    public void CardMix()   // 카드 섞기 메서드
    {
        switch (level)  // 난이도 별 필드 초기화
        {
            case Level.Easy:
                revealedCards = new string[2, 4];
                mixedCards = new int[2, 4];
                randomedCards = new bool[8];
                tryLimit = 10;
                break;
            case Level.Normal:
                revealedCards = new string[4, 4];
                mixedCards = new int[4, 4];
                randomedCards = new bool[16];
                tryLimit = 20;
                break;
            case Level.Hard:
                revealedCards = new string[4, 6];
                mixedCards = new int[4, 6];
                randomedCards = new bool[24];
                tryLimit = 30;
                break;
        }

        tryCount = 0;
        foundCount = 0;
        haveToFoundCount = randomedCards.Length/2;
        gameResult = Result.Undecided;

        Console.WriteLine("\n카드를 섞는 중...");

        for (int i = 0; i < revealedCards.GetLength(0); i++)
        {
            for (int j = 0; j < revealedCards.GetLength(1); j++)
            {
                revealedCards[i, j] = "**";

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

    public void MiriBogi()
    {

    }

    public void ShowCards()
    {
        Console.Write("\n    ");

        for (int i = 0; i < revealedCards.GetLength(1); i++)
        {
            Console.Write($"{i + 1}열 ");
        }

        Console.WriteLine();

        for (int i = 0; i < revealedCards.GetLength(0); i++)
        {
            Console.Write($"{i+1}행 ");

            for (int j = 0; j < revealedCards.GetLength(1); j++)
            {
                Console.Write($"{revealedCards[i, j]}  ");
            }

            Console.WriteLine();
        }
    }

    public void ShowCount()
    {
        Console.WriteLine($"\n시도 횟수: {tryCount}/{tryLimit} | 찾은 쌍: {foundCount}/{haveToFoundCount}");
    }

    public void SelectCards()
    {
        while (true)
        {
            Console.Write("\n첫 번째 카드를 선택하세요 (행 열): ");
            input = Console.ReadLine().Trim();

            if (input.Split(' ').Length == 2 && int.TryParse(input.Split(' ')[0], out int result) && int.TryParse(input.Split(' ')[1], out int result2))
            {
                haeng1 = result - 1;
                yeol1 = result2 - 1;

                if (haeng1 >= 0 && haeng1 < mixedCards.GetLength(0) && yeol1 >= 0 && yeol1 < mixedCards.GetLength(1))
                    if (revealedCards[haeng1, yeol1] == "**")
                        break;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }

        revealedCards[haeng1, yeol1] = $"[{mixedCards[haeng1, yeol1].ToString()}]";
        ShowCards();

        while (true)
        {
            Console.Write("\n두 번째 카드를 선택하세요 (행 열): ");
            input = Console.ReadLine().Trim();

            if (input.Split(' ').Length == 2 && int.TryParse(input.Split(' ')[0], out int result) && int.TryParse(input.Split(' ')[1], out int result2))
            {
                haeng2 = result - 1;
                yeol2 = result2 - 1;

                if (haeng2 >= 0 && haeng2 < mixedCards.GetLength(0) && yeol2 >= 0 && yeol2 < mixedCards.GetLength(1))
                    if (revealedCards[haeng2, yeol2] == "**")
                        break;
            }

            Console.WriteLine("잘못된 입력입니다.");
        }

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

    public void IsGameEnd()
    {
        if (foundCount == haveToFoundCount)
        {
            Console.WriteLine("\n=== 게임 클리어! ===");
            Console.WriteLine($"총 시도 횟수: {tryCount}");
            gameResult = Result.Win;
            return;
        }

        else if (tryCount == tryLimit)
        {
            Console.WriteLine("\n=== 게임 오버! ===");
            Console.WriteLine($"시도 횟수를 모두 사용했습니다.");
            Console.WriteLine($"찾은 쌍: {foundCount}/{haveToFoundCount}");
            gameResult = Result.Lose;
            return;
        }
    }

    public bool GameEnd()
    {
        while (true)
        {
            Console.Write("\n새 게임을 하시겠습니까? (Y/N): ");
            input = Console.ReadLine();

            if (input == "n" || input == "N")
            {
                Console.WriteLine("\n게임을 종료합니다.");
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

enum Result
{
    Undecided,
    Win,
    Lose    
}