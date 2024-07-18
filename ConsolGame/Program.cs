namespace ConsolGame
{
    internal class Program
    {
        #region struct(구조체)
        struct GameData
        {
            public bool isRunning;

            public bool[,] map;
            public Point mapScale;
            public ConsoleKey inputKey;

            public Player player;
            public Point tailItem;
        }

        struct Player
        {
            public List<Point> points;  //신체가 담긴 배열 *신체(머리, 몸통, 꼬리)
            public int length;          //신체 총 길이
        }

        struct Point
        {
            public int x;
            public int y;
        }
        #endregion

        static GameData data;

        static void Main(string[] args)
        {
            Start();

            while (data.isRunning)
            {
                Render();
                Run();
            }

            PrintEnd();
        }

        static void Start()
        {
            Console.CursorVisible = false;
            data = new GameData();
            data.isRunning = true;

            PrintInsert();

            //사용자가 직접 맵 크기 정하기
            //PrintQueryWithRule(out data.mapScale.x, out data.mapScale.y);

            #region 맵 초기화
            data.mapScale.y = 20;
            data.mapScale.x = 20;
            data.map = new bool[data.mapScale.y, data.mapScale.x];

            //맵 외각 참,거짓 나누기
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    data.map[y, x] = (x == 0 || x == data.mapScale.x - 1) || (y == 0 || y == data.mapScale.y - 1) ? false : true;
                }
            }
            #endregion

            #region 플레이어 초기화
            data.player.length = 3;
            data.player.points = new List<Point>();
            for (int i = 0; i < data.player.length; i++)
            {
                data.player.points.Add(new Point() { x = 3, y = 3 + i });
            }
            #endregion

            //꼬리 아이템 초기화
            data.tailItem = new Point() { x = 10, y = 10 };
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();
            PrintPlayer();
            printTailItem();
        }

        static void Run()
        {
            Input();
            Update();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            Move();
            AddTail();
        }

        #region Print*
        static void PrintInsert()
        {
            Console.WriteLine("┌──────────────┐");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("│  Snake Game  │");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("└──────────────┘");
            Console.WriteLine();
            Console.WriteLine(" 계속하려면 아무키나 누르세요 ");
            Console.ReadKey();

            Console.Clear();
        }
        static void PrintQueryWithRule(out int _x, out int _y)
        {
            bool isX, isY;
            Console.WriteLine("====================게임 방법====================\n");
            Console.WriteLine("W, ↑ : 위로 이동\t| S, ↓ : 아래로 이동\nA, ← : 왼쪽으로 이동\t| D, → : 오른쪽으로 이동\n");
            Console.WriteLine("=================================================\n");
            Console.WriteLine("플레이할 맵의 크기");

            while (true)
            {
                Console.Write("X : ");
                isX = int.TryParse(Console.ReadLine(), out _x);
                Console.Write("Y : ");
                isY = int.TryParse(Console.ReadLine(), out _y);

                if ((isX && isY) && (_x > 0 && _y > 0))
                    break;
                else
                    Console.WriteLine("잘못된 수를 입력하였습니다.");
            }
        }
        static void PrintEnd()
        {
            Console.Clear();

            Console.WriteLine("┌──────────────┐");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("│   THE  END   │");
            Console.WriteLine("│              │");
            Console.WriteLine("│              │");
            Console.WriteLine("└──────────────┘");
            Console.WriteLine();
        }

        static void PrintMap()
        {
            //맵 외각 생성
            for (int y = 0; y < data.map.GetLength(0); y++)
            {
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.map[y, x])
                        Console.Write(" ");
                    else
                    {
                        PrintTextColor(ConsoleColor.Blue, "o");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine($"현재 길이 : {data.player.length}\n");
            PrintTextColor(ConsoleColor.Green, "H");
            Console.Write(" : 머리, o : 몸통, ");
            PrintTextColor(ConsoleColor.Yellow, "t");
            Console.Write(" : 꼬리, ");
            PrintTextColor(ConsoleColor.Yellow, "T");
            Console.WriteLine(" : 아이템");
            Console.WriteLine("W, ↑ : 위로 이동 | S, ↓ : 아래로 이동\nA, ← : 왼쪽으로 이동 | D, → : 오른쪽으로 이동 ");
        }

        static void PrintPlayer()
        {
            for (int i = 0; i < data.player.length; i++)
            {
                Console.SetCursorPosition(data.player.points[i].x, data.player.points[i].y);
                if (i == 0)
                {
                    PrintTextColor(ConsoleColor.Green, "H");
                }
                else if (i == data.player.length - 1)
                {
                    PrintTextColor(ConsoleColor.Yellow, "t");
                }
                else
                {
                    Console.Write("o");
                }
            }
        }

        static void printTailItem()
        {
            Console.SetCursorPosition(data.tailItem.x, data.tailItem.y);
            PrintTextColor(ConsoleColor.Yellow, "T");
        }
        static void PrintTextColor(ConsoleColor _color, string _msg)
        {
            Console.ForegroundColor = _color;
            Console.Write(_msg);
            Console.ResetColor();
        }
        #endregion

        #region Move
        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.player.points[0].x, y = data.player.points[0].y - 1 };

            if (data.map[next.y, next.x])
            {
                if (CheckHeadPoint(next))
                {
                    data.isRunning = false;
                    return;
                }

                data.player.points.Insert(0, next);
                if (data.player.points.Count != data.player.length)
                {
                    data.player.points.RemoveAt(data.player.length);
                }
            }
        }
        static void MoveDown()
        {
            Point next = new Point() { x = data.player.points[0].x, y = data.player.points[0].y + 1 };

            if (data.map[next.y, next.x])
            {
                if (CheckHeadPoint(next))
                {
                    data.isRunning = false;
                    return;
                }

                data.player.points.Insert(0, next);
                if (data.player.points.Count != data.player.length)
                {
                    data.player.points.RemoveAt(data.player.length);
                }
            }
        }
        static void MoveLeft()
        {
            Point next = new Point() { x = data.player.points[0].x - 1, y = data.player.points[0].y };

            if (data.map[next.y, next.x])
            {
                if (CheckHeadPoint(next))
                {
                    data.isRunning = false;
                    return;
                }

                data.player.points.Insert(0, next);
                if (data.player.points.Count != data.player.length)
                {
                    data.player.points.RemoveAt(data.player.length);
                }
            }
        }
        static void MoveRight()
        {
            Point next = new Point() { x = data.player.points[0].x + 1, y = data.player.points[0].y };

            if (data.map[next.y, next.x])
            {
                if (CheckHeadPoint(next))
                {
                    data.isRunning = false;
                    return;
                }

                data.player.points.Insert(0, next);
                if (data.player.points.Count != data.player.length)
                {
                    data.player.points.RemoveAt(data.player.length);
                }
            }
        }

        //자기의 신체와 충돌하는지 체크
        static bool CheckHeadPoint(Point _point)
        {
            //리스트 내에서 값이 존재하는지 체크 있으면 참 없으면 거짓
            return data.player.points.Contains(_point);
        }
        #endregion

        #region Tail
        //아이템인 꼬리를 획득
        static void AddTail()
        {
            if (data.tailItem.x == data.player.points[0].x &&
                data.tailItem.y == data.player.points[0].y)
            {
                data.player.points.Add(LastPoint(data.tailItem));
                data.player.length++;

                data.tailItem = RandomPoint();
            }
        }

        //어느쪽에서 생성될지
        static Point LastPoint(Point _tail)
        {
            //마지막, 뒤에서 2번째
            Point lastTail = data.player.points.LastOrDefault();
            Point lastOfSecond = data.player.points.ElementAt(data.player.length - 2);

            if (lastOfSecond.x > lastTail.x)        //왼쪽생성
                _tail = new Point() { x = lastTail.x - 1, y = lastTail.y };
            else if (lastOfSecond.x < lastTail.x)   //오른쪽생성
                _tail = new Point() { x = lastTail.x + 1, y = lastTail.y };
            else if (lastOfSecond.y > lastTail.y)   //아래쪽생성
                _tail = new Point() { x = lastTail.x, y = lastTail.y - 1 };
            else if (lastOfSecond.y < lastTail.y)   //위쪽생성
                _tail = new Point() { x = lastTail.x, y = lastTail.y + 1 };

            return _tail;
        }

        //아이템인 꼬리를 맴 랜덤된 빈공간에서 출현
        static Point RandomPoint()
        {
            Random rand = new Random();

            Point newPoint = new Point();

            newPoint.x = rand.Next(1, data.mapScale.x - 1); //1 <= x < 19
            newPoint.y = rand.Next(1, data.mapScale.x - 1);

            for (int i = 0; i < data.player.length; i++)
            {
                if (newPoint.x == data.player.points[i].x &&
                    newPoint.y == data.player.points[i].y)
                {
                    newPoint.x = rand.Next(1, data.mapScale.x - 1);
                    newPoint.y = rand.Next(1, data.mapScale.x - 1);
                }
            }

            return newPoint;
        }
        #endregion
    }
}