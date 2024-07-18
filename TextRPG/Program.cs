

namespace TextRPG
{
    internal class Program
    {
        enum Scene { Select, Confirm, Intro, Street, Inn, Store, Amhurst, SouthPerry, LithHarbor }
        enum Job { Beginner, Warrior, Archer, Mage, Rogue }
        enum Type { Main, Sub, Nomal }

        #region struct
        struct GameData
        {
            public bool isRunning;

            public Scene scene;

            public Player player;
            public Enemy[] enemys;
            public Quest quest;
        }

        struct Player
        {
            public string name;
            public Job job;
            public State state;
            public int[] maxExps;
            public int gold;
        }

        struct Enemy
        {
            public string name;
            public int level;
            public float exp;
            public int hp;
            public Drop dropItem;
        }

        struct Quest
        {
            public Type type;
            public string name;
            public string npcName;
            public string description;
        }

        struct State
        {
            public int level;
            public float curExp;
            public int curHp;
            public int maxHp;
            public int STR;
            public int DEX;
            public int INT;
            public int LUK;
        }
        struct Drop
        {
            public int gold;
            public Item item;
        }
        struct Item
        {
            public string name;
            public string description;
        }
        #endregion

        static GameData data = new GameData();
        //static Player player = data.player;
        //static State state = data.player.state;

        static void Main(string[] args)
        {
            Start();

            while (data.isRunning)
            {
                Run();
            }
        }

        static void Start()
        {
            data.isRunning = true;

            PrintInsert();
        }

        private static void Run()
        {
            Console.Clear();

            switch (data.scene)
            {
                case Scene.Select:
                    SelectScene();
                    break;
                case Scene.Confirm:
                    ConfirmScene();
                    break;
                case Scene.Intro:
                    IntroScene();
                    break;
                case Scene.Street:
                    StreetScene();
                    break;
                case Scene.Store:
                    StoreScene();
                    break;
                case Scene.Inn:
                    InnScene();
                    break;
                case Scene.Amhurst:
                    AmhurstScene();
                    break;
                case Scene.SouthPerry:
                    SouthPerryScene();
                    break;
            }
        }

        static void Wait(float seconds)
        {
            Thread.Sleep((int)(seconds * 1000));
        }

        static void PrintInsert()
        {
            Console.WriteLine("┌──────────────┐");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("│  Maple Story │");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("└──────────────┘");
            Console.WriteLine();
            Console.WriteLine(" 계속하려면 아무키나 누르세요 ");
            Console.ReadKey();
        }
        static void PrintGetState()
        {
            Console.WriteLine("캐릭터 스텟이 정해지는 중입니다.\n");
            Console.WriteLine("(주사위 굴러가는 소리) 대굴... 대굴...");
            RandomGetStets(out data.player.state.STR, out data.player.state.DEX, out data.player.state.INT, out data.player.state.LUK);

            data.player.job = Job.Beginner;
            data.player.gold = 100;
            data.player.maxExps = new int[10];
            data.player.state.level = 1;
            data.player.state.curExp = 0.0f;
            data.player.state.maxHp = 100;
            data.player.state.curHp = data.player.state.maxHp;
            
            Wait(3);
        }
        static void PrintState()
        {
            Console.Clear();

            Console.WriteLine("========캐릭터 상태창========");
            Console.WriteLine($"이름\t: {data.player.name}");
            Console.WriteLine($"직업\t: {data.player.job}");
            Console.WriteLine($"레벨\t: {data.player.state.level}");
            Console.WriteLine($"경험치\t: {data.player.state.curExp}");
            Console.WriteLine($"체력\t: {data.player.state.maxHp}");
            Console.WriteLine($"힘\t: {data.player.state.STR}\t| 민첩 : {data.player.state.DEX}");
            Console.WriteLine($"지력\t: {data.player.state.INT}\t| 운   : {data.player.state.LUK}");
            Console.WriteLine($"소지 골드 : {data.player.gold} gold");
            Console.WriteLine("=============================\n");
        }

        static void PrintIntro()
        {
            Console.WriteLine("당신은 거대한 단풍나무 아래에서 눈이 떠졌습니다.");
            Console.WriteLine("단풍나무는 거대한 언덕 위에 있었습니다.");
            Console.WriteLine("눈이 떠진 당신은 일어나서 주위를 둘러봅니다.");
            Console.WriteLine("주위를 둘러보니 멀리 마을 하나가 보입니다.");
            Console.WriteLine("당신은 언덕에서 내려와 아까 보였던 마을을 향해 걸어갑니다.");

            Wait(10);
        }

        static void PrintStreet()
        {
            Console.WriteLine("\n┃ 당신은 현재 길거리를 걷고 있습니다.\t┃");
            Console.WriteLine("┃ 앞에 세 갈래길이 나왔습니다.\t\t┃");
            Console.WriteLine("┃ 어느 방향으로 가시겠습니까?\t\t┃");
            Console.WriteLine();
            Console.WriteLine("1. 왼쪽 / 2. 가운데 / 3. 오른쪽");
            Console.WriteLine("---------------------------------");
            Console.Write("입력 : ");
        }

        static void GetMonsterInformation()
        {
            data.enemys = new Enemy[2];

            //이름
            data.enemys[0].name = "달팽이";
            data.enemys[1].name = "슬라임";

            //레벨
            data.enemys[0].level = 1;
            data.enemys[1].level = 5;

            //경험치
            data.enemys[0].exp = 5;
            data.enemys[1].exp = 15;

            //체력
            data.enemys[0].hp = 5;
            data.enemys[1].hp = 30;

        }

        static void SelectScene()
        {
            Console.Write("캐릭터 이름을 정하시오 : ");
            data.player.name = Console.ReadLine();
            if (data.player.name == "")
            {
                Console.WriteLine("잘못 입력하셨습니다.");
                return;
            }
            data.scene = Scene.Confirm;
        }

        static void RandomGetStets(out int _str, out int _dex, out int _int, out int _luk)
        {
            Random rand = new Random();

            _str = rand.Next(4, 13);
            _dex = rand.Next(4, 13);
            _int = rand.Next(4, 13);
            _luk = rand.Next(4, 13);
        }

        static void ConfirmScene()
        {
            PrintGetState();
            PrintState();

            Console.WriteLine("이대로 플레이 하시겠습니까?\n");
            Console.WriteLine("Y - 이대로 플레이 / N - 스텟을 다시 돌린다.");
            Console.WriteLine("-----------------------------------------");
            Console.Write("입력 : ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "Y":
                case "y":
                    Console.Clear();
                    Console.WriteLine("인트로로 넘어감니다...");
                    Wait(2);
                    data.scene = Scene.Intro;
                    break;
                case "N":
                case "n":
                    data.scene = Scene.Confirm;
                    break;
                default:
                    data.scene = Scene.Confirm;
                    break;
            }
        }

        static void IntroScene()
        {
            PrintIntro();
            GetMonsterInformation();
            data.scene = Scene.Street;
        }
        static void StreetScene()
        {
            Console.Clear();
            PrintStreet();
            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                case "왼":
                case "왼쪽":
                    Console.Clear();
                    ChoiceLeftStreet();
                    break;
                case "2":
                case "앞":
                case "가운데":
                    Console.Clear();
                    ChoiceCenterStreet();
                    break;
                case "3":
                case "오":
                case "오른쪽":
                    Console.Clear();
                    ChoiceRightStreet();
                    break;
                default:
                    data.scene = Scene.Street;
                    break;
            }
        }
        static void ChoiceLeftStreet()
        {
            Console.ReadKey();
        }
        static void ChoiceCenterStreet()
        {
            Console.ReadKey();
        }
        static void ChoiceRightStreet()
        {
            Console.ReadKey();
        }

        static void StoreScene()
        {

        }
        static void InnScene()
        {

        }
        static void AmhurstScene()
        {

        }
        static void SouthPerryScene()
        {

        }
    }
}
