
using System;
using System.Threading;

class CardGame
{
    Cards cards;

    static Level level;
    static Mode mode;
    static public Skin skin;
    static public Result gameResult = Result.Undecided;

    static public int tryCount;
    static public int foundCount;
    static public int haveToFoundCount;
    static public int tryLimit;

    string input;

    int sleepMSeconds;

    public void Setting()
    {
        while (true)    // 난이도 세팅
        {
            Console.WriteLine("\n난이도를 선택하세요:\n1. 쉬움 (2x4)\n2. 보통 (4x4)\n3. 어려움 (4x6)");
            Console.Write("선택: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    level = Level.Easy;
                    break;
                case "2":
                    level = Level.Normal;
                    break;
                case "3":
                    level = Level.Hard;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            break;
        }

        while (true)    // 카드 스킨 세팅
        {
            Console.WriteLine("\n스킨을 선택하세요:\n1. 숫자 (기본)\n2. 알파벳 (컬러)\n3. 기호 (컬러)");
            Console.Write("선택: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    skin = Skin.Number;
                    return;
                case "2":
                    skin = Skin.Alphabet;
                    return;
                case "3":
                    skin = Skin.Sign;
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
        }
    }

    public void ResetGame()
    {
        cards = new Cards(level, skin, out int numberOfCards); // 레벨 기준으로 카드 초기화

        switch (level)  // 난이도 별 카드게임 필드 초기화
        {
            case Level.Easy:
                tryLimit = 10;
                sleepMSeconds = 5000;
                break;

            case Level.Normal:
                tryLimit = 20;
                sleepMSeconds = 3000;
                break;

            case Level.Hard:
                tryLimit = 30;
                sleepMSeconds = 2000;
                break;
        }

        tryCount = 0;
        foundCount = 0;
        haveToFoundCount = numberOfCards / 2;
        gameResult = Result.Undecided;

        cards.Mix();
    }

    public void PreView()
    {
        Console.Clear();

        cards.RevealAll();
        ShowCards();

        Console.WriteLine($"\n잘 기억하세요! {sleepMSeconds/1000}초 후 뒤집힙니다.");
        Thread.Sleep(sleepMSeconds);

        cards.HideAll();
    }

    public void ShowCards()
    {
        Console.Clear();
        cards.ShowRevealedCards(skin);
    }

    public void ShowCount()
    {
        Console.WriteLine($"\n시도 횟수: {tryCount}/{tryLimit} | 찾은 쌍: {foundCount}/{haveToFoundCount}");
    }

    public void SelectCards()
    {
        int row1, column1;

        while (true)
        {
            Console.Write("\n첫 번째 카드를 선택하세요 (행 열): ");
            input = Console.ReadLine().Trim();

            if (input.Split(' ').Length == 2 && int.TryParse(input.Split(' ')[0], out int result) && int.TryParse(input.Split(' ')[1], out int result2))
            {
                row1 = result - 1;
                column1 = result2 - 1;

                if (!cards.IsOutOfRangeOrAlreadyRevealed(row1, column1))
                {
                    break;
                }
            }

            Console.WriteLine("잘못된 입력입니다.");
        }

        cards.RevealCard(row1, column1);
        ShowCards();

        int row2, column2;

        while (true)
        {
            Console.Write("\n두 번째 카드를 선택하세요 (행 열): ");
            input = Console.ReadLine().Trim();

            if (input.Split(' ').Length == 2 && int.TryParse(input.Split(' ')[0], out int result) && int.TryParse(input.Split(' ')[1], out int result2))
            {
                row2 = result - 1;
                column2 = result2 - 1;

                if (!cards.IsOutOfRangeOrAlreadyRevealed(row2, column2))
                {
                    break;
                }
            }

            Console.WriteLine("잘못된 입력입니다.");
        }

        cards.RevealCard(row2, column2);
        ShowCards();

        if (cards.IsMatched(row1, column1, row2, column2))
        {
            Console.WriteLine("\n짝을 찾았습니다!");
            foundCount++;

            Thread.Sleep(2000);
        }

        else
        {
            Console.WriteLine("\n짝이 맞지 않습니다!");
            cards.HideCard(row1, column1);
            cards.HideCard(row2, column2);

            Thread.Sleep(2000);
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