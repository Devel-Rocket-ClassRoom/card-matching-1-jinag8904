
using System;
using System.Threading;

abstract class CardGame
{
    Cards cards;

    protected static int tryCount;    // 시도 횟수
    protected static int foundCount;  // 찾은 쌍
    protected static int haveToFoundCount;    // 찾아야 하는 쌍
    protected static int tryLimit;    // 시도 제한

    int sleepMSeconds;

    string input;

    public virtual void ResetGame()
    {
        cards = new Cards(GameTable.level, GameTable.skin, out int numberOfCards); // 레벨 기준으로 카드 초기화

        switch (GameTable.level)
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

        GameTable.gameResult = Result.Undecided;

        cards.Mix();
    }

    public virtual void PreView()
    {
        Console.Clear();

        cards.RevealAll();
        ShowCards();

        Console.WriteLine($"\n잘 기억하세요! {sleepMSeconds / 1000}초 후 뒤집힙니다.");
        Thread.Sleep(sleepMSeconds);

        cards.HideAll();
    }

    public void ShowCards()
    {
        Console.Clear();
        cards.ShowRevealedCards(GameTable.skin);
    }

    public virtual void ShowCount()
    {
        Console.WriteLine($"\n시도 횟수: {tryCount}/{tryLimit} | 찾은 쌍: {foundCount}/{haveToFoundCount}");
    }

    public bool SelectOneCard(ref int row, ref int column)
    {
        Console.Write(" 카드를 선택하세요. (행 열): ");
        input = Console.ReadLine().Trim();

        if (input.Split(' ').Length == 2 && int.TryParse(input.Split(' ')[0], out int result) && int.TryParse(input.Split(' ')[1], out int result2))
        {
            row = result - 1;
            column = result2 - 1;

            if (!cards.IsOutOfRangeOrAlreadyRevealed(row, column))
            {
                return false;
            }
        }

        Console.WriteLine("잘못된 입력입니다.");
        return true;
    }

    public virtual bool SelectCards()
    {
        int row1 = 0, column1 = 0;

        do
        {
            Console.Write("\n첫 번째");
        }
        while (SelectOneCard(ref row1, ref column1));

        cards.RevealCard(row1, column1);
        ShowCards();

        int row2 = 0, column2 = 0;

        do
        {
            Console.Write("\n두 번째");
        }
        while (SelectOneCard(ref row2, ref column2));

        cards.RevealCard(row2, column2);
        ShowCards();

        tryCount++;

        if (cards.Matching(row1, column1, row2, column2))
        {
            Console.WriteLine("\n짝을 찾았습니다!");
            foundCount++;
            Thread.Sleep(2000);
            return true;
        }

        else
        {
            Console.WriteLine("\n짝이 맞지 않습니다!");
            cards.HideCard(row1, column1);
            cards.HideCard(row2, column2);
            Thread.Sleep(2000);
            return false;
        }
    }

    public void CheckGameEnd()
    {
        if (IsGameOver())
        {
            GameTable.gameResult = Result.Lose;
            Console.WriteLine("\n=== 게임 오버! ===");
            Console.WriteLine($"{GetStatusText()}");
            Console.WriteLine($"찾은 쌍: {foundCount}/{haveToFoundCount}");
        }

        else if (foundCount == haveToFoundCount)
        {
            GameTable.gameResult = Result.Win;
            Console.WriteLine("\n=== 게임 클리어! ===");
            Console.WriteLine($"총 시도 횟수: {tryCount}");
        }
    }

    public virtual bool GameEnd()
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

    protected abstract bool IsGameOver();
    protected abstract string GetStatusText();
}