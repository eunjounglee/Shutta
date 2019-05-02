using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shutta
{
    public class BasicPlayer
    {

        public BasicPlayer(int money)
        {
            Money = money;
            _cards = new List<Card>();
            _firstcards = new List<Card>();
        }

        public int Money { get; set; }

        private readonly List<Card> _cards;
        private readonly List<Card> _firstcards;

        public void AddCard(Card card)
        {
            _cards.Add(card);
            _firstcards.Add(card);
        }


        public virtual void CalculateScore()
        {
            if (_cards[0].No == _cards[1].No)
                Score = _cards[0].No * 10; // 10 ~ 100
            else
                Score = (_cards[0].No + _cards[1].No) % 10; // 0 ~ 9
        }

        public virtual void OrderScore()
        {
            Score = _firstcards[0].No; // 카드숫자
        }


        public int Score { get; set; }

        // indexer
        public Card this[int index]
        {
            get
            {
                return _cards[index];
            }

        }

        public void PrepareRound()
        {
            _cards.Clear();
            Score = 0;
        }
    }
}
