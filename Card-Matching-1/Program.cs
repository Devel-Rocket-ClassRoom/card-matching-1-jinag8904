using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
CardGame cardgame = new CardGame();
bool again = true;

while (again)
{
    cardgame.Setting();
    cardgame.Reset();
    cardgame.CardMix();
    cardgame.MiriBogi();

    while (true)
    {
        cardgame.ShowCards();
        cardgame.ShowCount();

        cardgame.SelectCards();

        cardgame.IsGameEnd();

        if (cardgame.gameResult != Result.Undecided)
        {
            again = cardgame.GameEnd();
            break;
        }
    }
}