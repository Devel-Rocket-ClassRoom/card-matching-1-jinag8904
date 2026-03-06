using System;

bool again = true;

Console.WriteLine("=== 카드 짝 맞추기 게임 ===\n");
Card card = new Card();

while (again)
{
    card.CardMix();

    while (true)
    {
        card.ShowCards();
        card.ShowCount();

        card.SelectCards();

        if (card.foundCount == 8)
        {
            again = card.GameEnd();
        }

        if (!again) return;
    }
}