using System;

CardGame classicGame = new ClassicGame();
CardGame timeAttackGame = new TimeAttackGame();
CardGame survivalGame = new SurvivalGame();
CardGame cardgame;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");

bool again = true;

do
{
    GameTable.Setting();

    switch (GameTable.mode)
    {
        case Mode.Classic:
            cardgame = classicGame;
            break;
        case Mode.TimeAttack:
            cardgame = timeAttackGame;
            break;
        case Mode.Survival:
            cardgame = survivalGame;
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