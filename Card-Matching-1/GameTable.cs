using System;

class GameTable
{
    public static Mode mode;
    public static Level level;
    static public Skin skin;
    static public Result gameResult = Result.Undecided;

    static string input;

    static public void Setting()
    {
        while (true)    // 모드 세팅
        {
            Console.WriteLine("\n게임 모드를 선택하세요:\n1. 클래식\n2. 타임어택\n3. 서바이벌");
            Console.Write("선택: ");
            input = Console.ReadLine().Trim();

            switch (input)
            {
                case "1":
                    mode = Mode.Classic;
                    break;
                case "2":
                    mode = Mode.TimeAttack;
                    break;
                case "3":
                    mode = Mode.Survival;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            break;
        }

        while (true)    // 난이도 세팅
        {
            Console.WriteLine("\n난이도를 선택하세요:\n1. 쉬움\n2. 보통\n3. 어려움");
            Console.Write("선택: ");
            input = Console.ReadLine().Trim();

            switch (input)
            {
                case "1":
                    level = Level.Easy;
                    break;
                case "2":
                    level = Level.Normal;
                    break;
                case "3":
                    level = Level.Hard;
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            break;
        }

        while (true)    // 카드 스킨 세팅
        {
            Console.WriteLine("\n스킨을 선택하세요:\n1. 숫자 (기본)\n2. 알파벳 (컬러)\n3. 기호 (컬러)");
            Console.Write("선택: ");
            input = Console.ReadLine().Trim();

            switch (input)
            {
                case "1":
                    skin = Skin.Number;
                    return;
                case "2":
                    skin = Skin.Alphabet;
                    return;
                case "3":
                    skin = Skin.Sign;
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    continue;
            }
        }
    }
}