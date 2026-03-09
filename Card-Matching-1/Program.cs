using System;

CardGame cardgame;
CardGame[] cardGames =
{
    new ClassicGame(),
    new TimeAttackGame(),
    new SurvivalGame()
};

bool again = true;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");

do
{
    GameTable.Setting();

    switch (GameTable.mode)
    {
        case Mode.Classic:
            cardgame = cardGames[0];
            break;
        case Mode.TimeAttack:
            cardgame = cardGames[1];
            break;
        case Mode.Survival:
            cardgame = cardGames[2];
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