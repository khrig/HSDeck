namespace HS {
    public class Card {
        private CardData cardData;
        public Card() { }
        public Card(CardData cardData) {
            this.cardData = cardData;
        }

        public string Title { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
        public string ImageUrl { get; set; }
        public string HttpImageUrl { get { return cardData.ImageUrl; } }
        public int Cost { get { return cardData.Cost; } }

        public void SetCardData(CardData cardData) {
            this.cardData = cardData;
        }

        public string ListDisplay {
            get { return string.Format("[{0}] ({1}) - {2}", Count, Cost, Title); }
        }
    }
}