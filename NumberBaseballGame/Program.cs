namespace NumberBaseballGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int comNumber = 529;
            int answer;
            int inning = 1;
            int strick = 0;
            int ball = 0;
            int end = 0;

            int firstComNum, secondComNum, thirdComNum;
            int firstNum, secondNum, thirdNum;  //3자리 숫자 중 firstnum - 100의 자리수, secondnum - 10의 자리수, thirdnum - 1의 자리수
            
            firstComNum = (comNumber / 10) / 10;
            secondComNum = (comNumber / 10) % 10;
            thirdComNum = comNumber % 10;
            
            while (true)
            {
                Console.WriteLine("======================");
                Console.WriteLine($"\t{inning} 이닝");
                Console.WriteLine("======================");
            
                Console.Write("확인할 세자리 숫자 : ");
                string answerCheck = Console.ReadLine();
            
                if (!int.TryParse(answerCheck, out answer) || answer < 100 || answer > 999)
                {
                    Console.WriteLine("잘못된 입력을 하였습니다. 3자리 숫자로만 적어주세요");
                    continue;
                }
                else
                {
                    firstNum = (answer / 10) / 10;
                    secondNum = (answer / 10) % 10;
                    thirdNum = answer % 10;
                }
            
                if (firstNum == 0 || secondNum == 0 || thirdNum == 0)
                {
                    Console.WriteLine("3자리 중 0 이 들어가 있습니다.");
                    continue;
                }
                else if (firstNum == secondNum || firstNum == thirdNum || secondNum == thirdNum)
                {
                    Console.WriteLine("중복되는 숫자가 있습니다.");
                    continue;
                }
            
                //첫번째 자리 확인
                if (firstNum == firstComNum)
                { strick++; }
                else if (firstNum == secondComNum || firstNum == thirdComNum)
                { ball++; }
                else
                { end++; }
                //두번째 자리 확인
                if (secondNum == secondComNum)
                { strick++; }
                else if (secondNum == thirdComNum || secondNum == firstComNum)
                { ball++; }
                else
                { end++; }
                //세번째 자리 확인
                if (thirdNum == thirdComNum)
                { strick++; }
                else if (thirdNum == secondComNum || thirdNum == firstComNum)
                { ball++; }
                else
                { end++; }
            
                if (end == 3)
                {
                    Console.WriteLine("아웃!");
                    strick = ball = end = 0;
                    inning++;
                }
                else
                {
                    Console.WriteLine($"{strick} 스트라이크 / {ball} 볼\n");
            
                    if (strick == 3)
                    {
                        Console.WriteLine("플레이어의 승리입니다!!");
                        break;
                    }
                    else if (inning == 11)
                    {
                        Console.WriteLine("플레이어의 패배입니다!!");
                        break;
                    }
                    else
                    {
                        strick = ball = end = 0;
                        inning++;
                    }
                }
            }
        }
    }
}
