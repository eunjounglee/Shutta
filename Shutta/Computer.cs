using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Computer : Player
    {
        public Computer(int money) : base(money)
        {

        }

        public override CallType DecideCallType(List<Player> players, int index)
        {
            Player p = players[index];
            if (p.Score < 4)
            {
                return CallType.Die;
            }
            else if( p.Score < 10 ) {
                return CallType.Call;
            }
            else {
                return CallType.Betting;
            }
            throw new NotImplementedException();
        }
    }
}
