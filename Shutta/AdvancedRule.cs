using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    class AdvancedRule
    {
        private const int BetMoney = 100;
        public static void PrintMoney(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
                Console.WriteLine($"P{i} has \\{players[i].Money}");
        }

        public static bool CanRunRound(List<Player> players)
        {
            foreach (Player player in players)
                if (player.Money <= 0)
                    return false;

            return true;
        }

        public static int DecideOrder(List<Player> players)
        {
            foreach (Player player in players)
                player.PrepareRound();
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

            int winnerNo = FindWinner(players)[0];

            return winnerNo;
        }

        public static int RunRound(List<Player> players, int winnerNo)
        {
            // 각 선수가 이전 라운드에서 받은 카드를 클리어한다.
            foreach (Player player in players)
                player.PrepareRound();

            // 이전 라운드의 승자는 이번 라운드의 베팅 배수를 결정한다.
            // 단, 1라운드일 경우 선을 결정하여 베팅 배수를 결정한다.
            Console.WriteLine($"P[{winnerNo}] 는 이번 라운드의 배수를 선택하세요. (1: 1배, 2: 2배, 4: 4배, 8: 8배)");
            string inputText = Console.ReadLine();
            int input = int.Parse(inputText);
            MultipleType multipleType = (MultipleType)input;

            // 선수들이 학교를 간다
            int totalBetMoney = 0;

            foreach (Player player in players)
            {
                player.Money -= BetMoney * input;
                totalBetMoney += BetMoney * input;
            }
            
            // 딜러가 각 선수들에게 2장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (Player player in players)
                for (int i = 0; i < 2; i++)
                    player.AddCard(dealer.Draw());

            // 각 선수들이 가진 카드를 내림차순한다.
            foreach (Player player in players)
                players.OrderByDescending(x => x[0].No);

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                Player p = players[i];

                p.CalculateScore();
                Console.WriteLine($"P{i} ({p[0]}, {p[1]}) => {p.Score}");
            }

            // 승자와 패자를 가린다.



            // 승자에게 모든 베팅 금액을 준다.
            if ( FindWinner(players).Count >= 2)
            {
                players[FindWinner(players)[0]].Money += totalBetMoney / 2;
                players[FindWinner(players)[1]].Money += totalBetMoney / 2;

                winnerNo = AdvancedRule.DecideOrder(players);
                return winnerNo;
            }
            else
            { 
                winnerNo = FindWinner(players)[0];
                players[winnerNo].Money += totalBetMoney;
                return winnerNo;
            }
            
        }

        public static List<int> FindWinner(List<Player> players)
        {
            // return players.OrderByDescending(x => x.Score).First();
            int count = 0;
            List<int> playerIndex = new List<int>();
            int maxScore = 0;
            foreach (Player player in players)
                if (player.Score > maxScore)
                    maxScore = player.Score;

            foreach (Player player in players)
                if (player.Score == maxScore)
                {
                    playerIndex.Add(player.Index);
                    count++;
                }
            return playerIndex;
            // return null;
            throw new Exception("승자를 찾을 수 없음");
        }

    }
}
