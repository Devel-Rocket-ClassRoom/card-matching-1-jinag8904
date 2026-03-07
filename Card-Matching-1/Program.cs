using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
CardGame cardgame = new CardGame();
bool again = true;

do
{
    cardgame.Setting();
    cardgame.ResetGame();

    cardgame.PreView();

    while (true)
    {
        cardgame.ShowCards();
        cardgame.ShowCount();

        cardgame.SelectCards();

        cardgame.IsGameEnd();

        if (CardGame.gameResult != Result.Undecided)
        {
            again = cardgame.GameEnd();
            break;
        }
    }
} 
while (again);