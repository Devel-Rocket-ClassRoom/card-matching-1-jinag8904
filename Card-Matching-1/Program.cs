using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
CardGame cardgame;
bool again = true;

do
{
    GameTable.Setting();

    switch (GameTable.mode)
    {
        case Mode.Classic:
            cardgame = new ClassicGame();
            break;
        case Mode.TimeAttack:
            cardgame = new TimeAttackGame();
            break;
        case Mode.Survival:
            cardgame = new SurvivalGame();
            break;
        default:
            throw new ArgumentException("예외 발생");
    }

    cardgame.ResetGame();
    cardgame.PreView();

    while (true)
    {
        cardgame.ShowCards();
        cardgame.ShowCount();

        cardgame.SelectCards();

        cardgame.CheckGameEnd();

        if (GameTable.gameResult != Result.Undecided)
        {
            again = cardgame.GameEnd();
            break;
        }
    }
}
while (again);