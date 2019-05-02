using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{

    public class AdvancedPlayer : Player
    {

        public AdvancedPlayer(int money) : base(money)
        {

        }

  
        public override void CalculateScore()
        {
            if (_cards[0].No.Equals("3K") && _cards[1].No.Equals("8K"))
                    Score = 9999;
            if (_cards[0].No.Equals("8K") && _cards[1].No.Equals("3K"))
                    Score = 9999;
        }


        //public virtual void AdvancedCalculateScore()
        //{
        //    if (_cards[0].No == _cards[1].No)
        //        Score = _cards[0].No * 10; // 10 ~ 100
        //    else
        //        Score = (_cards[0].No + _cards[1].No) % 10; // 0 ~ 9

        //    if (_cards[0].No.Equals("3K"))
        //    {
        //        if (_cards[1].No.Equals("8K"))
        //            Score = 9999;
        //    }
        //    else if (_cards[0].No.Equals("8K"))
        //    {
        //        if (_cards[1].No.Equals("3K"))
        //            Score = 9999;
        //    }
        //}


    }
}
