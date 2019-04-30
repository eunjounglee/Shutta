using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shutta.UnitTest
{
    [TestClass] // attribute
    public class DealerTest
    {
        [TestMethod]
        public void 세_장의_광이_있어야_함()
        {
            int kwangCount = 0;

            Dealer dealer = new Dealer();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.Draw();

                if (card.IsKwang)
                    kwangCount++;
            }

            Assert.AreEqual(3, kwangCount);
        }

        [TestMethod]
        public void _1부터_10까지의_카드가_두_장씩_있어야_함()
        {
            int[] noCountArray = new int[11];

            Dealer dealer = new Dealer();
            for (int i = 0; i < 20; i++)
            {
                Card card = dealer.Draw();

                noCountArray[card.No]++;
            }

            for (int i = 1; i < noCountArray.Length; i++)
                Assert.AreEqual(2, noCountArray[i]);
        }

        [TestMethod]
        public void _1과_3과_8_한_장씩이_광이어야_함()
        {
            List<Card> cards = new List<Card>();

            Dealer dealer = new Dealer();
            for (int i = 0; i < 20; i++)
                cards.Add(dealer.Draw());

            Assert.AreEqual(true, HasKwang(1, cards));
            Assert.AreEqual(true, HasKwang(3, cards));
            Assert.AreEqual(true, cards.Exists(x => x.No == 8 && x.IsKwang));
        }

        private bool HasKwang(int no, List<Card> cards)
        {
            foreach (Card card in cards)
                if (card.No == no && card.IsKwang)
                    return true;

            return false;
        }
    }
}
