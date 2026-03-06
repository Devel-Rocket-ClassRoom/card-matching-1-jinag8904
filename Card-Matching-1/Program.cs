using System;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===");
Card card = new Card();
bool again = true;

while (again)
{
    card.LevelSetting();
    card.CardMix();

    card.MiriBogi();

    while (true)
    {
        card.ShowCards();
        card.ShowCount();

        card.SelectCards();

        card.IsGameEnd();

        if (card.gameResult != Result.Undecided)
        {
            again = card.GameEnd();
            break;
        }
    }
}