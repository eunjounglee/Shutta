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
        public const int SeedMoney = 500;
        private const int BetMoney = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("게임의 룰을 선택하세요. (1:기본룰, 2:확장룰)");
            string inputText = Console.ReadLine();
            int input = int.Parse(inputText);
            RuleType ruleType = (RuleType)input;

            Console.WriteLine("순서를 정합니다.");



            // 각 선수들이 시드 머니를 가진다.
            List<Player> players = new List<Player>();
            for (int i = 0; i < PlayerCount; i++)
            {
                if (ruleType == RuleType.Basic)
                    players.Add(new Player(SeedMoney));
                else if (ruleType == RuleType.Advanced)
                    players.Add(new AdvancedPlayer(SeedMoney));
            }



            int round = 1;
            RunOrder(players);
            // 선수 중 파산(오링)하는 사람이 있을 때 까지 라운드를 진행한다.
            while (true)
            {
                if (CanRunRound(players) == false)
                    break;


                Console.WriteLine($"[Round {round++}]");


                // 라운드를 진행한다
                RunRound(players);

                // 선수들이 가진 돈을 출력한다.
                PrintMoney(players);

                Console.WriteLine();
            }
        }




        private static void PrintMoney(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
                Console.WriteLine($"P{i} has \\{players[i].Money}");
        }

        private static bool CanRunRound(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money <= 0)
                    return false;

            return true;
        }


        static void RunOrder(List<Player> players)
        {

            // 딜러가 각 선수들에게 1장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                player.AddCard(dealer.FirstDraw());

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];
                p.OrderScore();
                Console.WriteLine($"P{i} ({p[0]}) => {p.Score}");
            }
        }

        static void RunRound(List<Player> players)
        {
            // 각 선수가 이전 라운드에서 받은 카드를 클리어한다.
            foreach (Player player in players)
                player.PrepareRound();


            // 선수들이 학교를 간다
            int totalBetMoney = 0;

            foreach (Player player in players)
            {
                player.Money -= BetMoney;
                totalBetMoney += BetMoney;
            }


            // 딜러가 각 선수들에게 2장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                for (int i = 0; i < 2; i++)
                    player.AddCard(dealer.Draw());


            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];

                p.CalculateScore();
                Console.WriteLine($"P{i} ({p[0]}, {p[1]}) => {p.Score}");
            }

             
            // 승자와 패자를 가린다.
            Player winner = FindWinner(players);


            //TODO : 승자가 1명 이상이면 베팅 머니를 돌려주고 라운드를 끝낸다.


            // 승자에게 모든 베팅 금액을 준다.
            winner.Money += totalBetMoney;
        }

        private static Player FindWinner(List<Player> players)
        {
            // return players.OrderByDescending(x => x.Score).First();
            int maxScore = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Score > maxScore)
                    maxScore = players[i].Score;
            }

            //foreach (Player player in players)
            //    if (player.Score > maxScore)
            //        maxScore = player.Score;

            if (players[0].Score == maxScore && players[1].Score == maxScore)
                CompareScore(players[0], players[1]);

            else if (players[1].Score == maxScore && players[2].Score == maxScore)
                CompareScore(players[1], players[2]);

            else if (players[2].Score == maxScore && players[0].Score == maxScore)
                CompareScore(players[2], players[0]);

            for (int i = 0; i < players.Count; i++)
                if (players[i].Score == maxScore)
                    return players[i];

            throw new Exception("승자를 찾을 수 없음.");

        }

        public static Object CompareScore(Player playerOne, Player playerTwo)
        {
            int CardOne = playerOne.ShowCard(1) > playerOne.ShowCard(2) ?
                playerOne.ShowCard(1) : playerOne.ShowCard(2);

            // 플레이어가 가진 카드를 높은 순서대로 정렬. 
            // return players.OrderByDescending(x => x.Score).First();

            int CardTwo = playerTwo.ShowCard(1) > playerTwo.ShowCard(2) ?
                playerTwo.ShowCard(1) : playerTwo.ShowCard(2);

            // 같은 숫자를 가진 경우에는 판 돈을 반으로 나눈다. 
            if (CardOne == CardTwo)
            {
                playerOne.Money += BetMoney / 2;
                playerTwo.Money += BetMoney / 2;
                return "승자가 두 명으로 게임을 다시 시작합니다.";
            }

            // 그 중 더 큰 숫자의 카드를 가진 사람이 이긴다. 
            else if (CardOne > CardTwo)
            {
                playerOne.Money += BetMoney;
                return playerOne;
            }
            else
            {
                playerTwo.Money += BetMoney;
                return playerTwo;
            }

        }
    }
