﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class Human : Player
    {
        public Human(int money) : base (money)
        {

        }

        public override void DecideBettingType()
        {

            throw new NotImplementedException();
        }
    }
}