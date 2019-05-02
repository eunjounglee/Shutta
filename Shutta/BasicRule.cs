using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class BasicRule : Rule
    {
        public static void PrintMoney(List<BasicPlayer> players)
        {
            for (int i = 0; i < players.Count; i++)
                Console.WriteLine($"P{i} has \\{players[i].Money}");
        }

        public static bool CanRunRound(List<BasicPlayer> players)
        {
            foreach (BasicPlayer player in players)
                if (player.Money <= 0)
                    return false;

            return true;
        }

        public static void RunRound(List<BasicPlayer> players)
        {
            // 각 선수가 이전 라운드에서 받은 카드를 클리어한다.
            foreach (BasicPlayer player in players)
                player.PrepareRound();

            // 선수들이 학교를 간다
            int totalBetMoney = 0;

            foreach (BasicPlayer player in players)
            {
                player.Money -= BetMoney;
                totalBetMoney += BetMoney;
            }

            // 딜러가 각 선수들에게 2장씩 카드를 돌린다
            Dealer dealer = new Dealer();
            foreach (BasicPlayer player in players)
                for (int i = 0; i < 2; i++)
                    player.AddCard(dealer.Draw());

            // 각 선수들의 족보를 계산하고 출력한다.
            for (int i = 0; i < players.Count; i++)
            {
                BasicPlayer p = players[i];

                p.CalculateScore();
                Console.WriteLine($"P{i} ({p[0]}, {p[1]}) => {p.Score}");
            }

            // 승자와 패자를 가린다.
            BasicPlayer winner = FindWinner(players);

            //TODO : 승자가 1명 이상이면 베팅 머니를 돌려주고 라운드를 끝낸다.


            // 승자에게 모든 베팅 금액을 준다.
            winner.Money += totalBetMoney;
        }

        public static BasicPlayer FindWinner(List<BasicPlayer> players)
        {
            // return players.OrderByDescending(x => x.Score).First();

            int maxScore = 0;
            foreach (BasicPlayer player in players)
                if (player.Score > maxScore)
                    maxScore = player.Score;

            foreach (BasicPlayer player in players)
                if (player.Score == maxScore)
                    return player;

            // return null;
            throw new Exception("승자를 찾을 수 없음");
        }
    }
}
