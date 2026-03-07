class ClassicGame : CardGame
{
    protected override bool IsGameOver()
    {
        if (tryCount == tryLimit)
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
        return "시도 횟수를 모두 사용했습니다.";
    }

}