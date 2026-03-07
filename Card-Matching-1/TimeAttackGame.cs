using System;
using System.Diagnostics;

class TimeAttackGame : CardGame
{
    public Stopwatch stopwatch = new Stopwatch();
    int limitMSeconds;

    public override void ResetGame()
    {
        base.ResetGame();

        switch (GameTable.level)
        {
            case Level.Easy:
                limitMSeconds = 60000;
                break;
            case Level.Normal:
                limitMSeconds = 90000;
                break;
            case Level.Hard:
                limitMSeconds = 120000;
                break;
        }
    }

    public override void PreView()
    {
        base.PreView();
        stopwatch.Start();
    }

    public override void ShowCount()
    {
        Console.WriteLine($"\n경과 시간: {stopwatch.ElapsedMilliseconds / 1000}초/{limitMSeconds / 1000}초 | 찾은 쌍: {foundCount}/{haveToFoundCount}");
    }

    protected override bool IsGameOver()
    {
        if (stopwatch.ElapsedMilliseconds > limitMSeconds)
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
        return "제한 시간을 초과했습니다.";
    }

    public override bool GameEnd()
    {
        stopwatch.Stop();
        stopwatch.Reset();
        return base.GameEnd();
    }
}