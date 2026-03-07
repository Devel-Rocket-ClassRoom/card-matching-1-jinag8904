using System;

class SurvivalGame : CardGame
{
    static int failedCount;
    static new int tryLimit;

    public override void ResetGame()
    {
        base.ResetGame();

        tryLimit = 3;
        failedCount = 0;
    }

    public override bool SelectCards()
    {
        if (base.SelectCards())
        {
            failedCount = 0;
            return true;
        }

        else
        {
            failedCount++;
            return false;
        }
    }

    public override void ShowCount()
    {
        Console.WriteLine($"\n시도 횟수: {failedCount}/{tryLimit} | 찾은 쌍: {foundCount}/{haveToFoundCount}");
    }

    protected override bool IsGameOver()
    {
        if (failedCount == tryLimit)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    protected override string GetStatusText()
    {
        return "연속으로 3번 틀렸습니다.";
    }
}