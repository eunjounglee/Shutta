using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class Program
    {
        public const int PlayerCount = 3;

        public const int SeedMoney = 1000;
        static void Main(string[] args)
        {
            Console.WriteLine("게임의 룰을 선택하세요. (1:기본룰, 2:확장룰)");
            string inputText = Console.ReadLine();
            int input = int.Parse(inputText);
            RuleType ruleType = (RuleType)input;

            // 각 선수들이 시드 머니를 가진다.
            if (ruleType == RuleType.Basic)
            {
                Console.WriteLine("기본룰 모드로 경기를 시작합니다.");
                List<BasicPlayer> players = new List<BasicPlayer>();
                for (int i = 0; i < PlayerCount; i++)
                {
                    players.Add(new BasicPlayer(SeedMoney));
                }

                int round = 1;
                // 선수 중 파산(오링)하는 사람이 있을 때 까지 라운드를 진행한다.
                while (true)
                {
                    if (BasicRule.CanRunRound(players) == false)
                        break;

                    Console.WriteLine($"[Round {round++}]");

                    // 라운드를 진행한다
                    BasicRule.RunRound(players);

                    // 선수들이 가진 돈을 출력한다.
                    BasicRule.PrintMoney(players);

                    Console.WriteLine();
                }
            }
            else if (ruleType == RuleType.Advanced)
            {
                Console.WriteLine("확장룰 모드로 경기를 시작합니다.");
                List<Player> players = new List<Player>();
                players.Add(new Human(SeedMoney));
                for (int i = 0; i < PlayerCount - 1 ; i++)
                {
                    players.Add(new Computer(SeedMoney));
                }

                int round = 1;
                int winnerNo = 0;
                
                winnerNo = AdvancedRule.DecideOrder(players);
                Console.WriteLine($" 선 플레이어는 P[{winnerNo}] 입니다.");
                // 선수 중 파산(오링)하는 사람이 있을 때 까지 라운드를 진행한다.
                while (true)
                {
                    if (AdvancedRule.CanRunRound(players) == false)
                    {
                        Console.WriteLine($"P[{AdvancedRule.FindBankrupt(players)}]는 파산자!");
                        break;
                    }
                       

                    Console.WriteLine($"[Round {round++}]");

                    // 라운드를 진행한다
                    winnerNo = AdvancedRule.RunRound(players, winnerNo);

                    // 선수들이 가진 돈을 출력한다.
                    AdvancedRule.PrintMoney(players);

                    Console.WriteLine();
                }
            }
        }
    }
}
