using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shutta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta.Tests
{
    [TestClass()]
    public class CardTests
    {
        [TestMethod()]
        public void 광이라면_숫자와_K()
        {
            Card card = new Card(3, true);
            Assert.AreEqual("3K", card.ToString());
        }

        [TestMethod()]
        public void 광이_아니라면_숫자()
        {
            Card card = new Card(3, false);
            Assert.AreEqual("3", card.ToString());
        }
    }
}