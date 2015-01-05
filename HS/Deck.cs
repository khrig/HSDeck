using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HS {
    public class Deck : IEnumerable<Card>
    {
        public string Title { get; set; }

        private ObservableCollection<Card> cards { get; set; }

        public Deck() {
            cards = new ObservableCollection<Card>();
            Title = "No Title";
        }

        public void AddCard(Card card) {
            cards.Add(card);
        }

        public void SortByCostAsc() {
            cards = new ObservableCollection<Card>(cards.OrderBy(card => card.Cost));
            //cards.Sort(new CostComparer());
        }

        public IEnumerator<Card> GetEnumerator() {
            return cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return cards.GetEnumerator();
        }
    }
}