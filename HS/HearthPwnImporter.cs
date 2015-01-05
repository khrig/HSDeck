using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HS
{
    public class HearthPwnImporter {
        private CardRepository rep;
        private CardArtImporter cardArtImporter;

        public event EventHandler updated;

        public HearthPwnImporter() {
            rep = new CardRepository();
            cardArtImporter = new CardArtImporter();
        }

        public Deck Import(string deckUrl) {
            string convertedUrl = ConvertUrl(deckUrl);

            WebClient wc = new WebClient();
            string deckHtml = wc.DownloadString(convertedUrl);
            List<string> lines = ParseLines(deckHtml);
            Deck deck = ParseDeck(lines);

            MergeCardData(deck);

            cardArtImporter.Import(deck);

            return deck;
        }

        private string ConvertUrl(string deckUrl) {
            //http://www.hearthpwn.com/decks/139450-1legend-eu-neptulon-shaman
            return deckUrl.Substring(0, deckUrl.IndexOf('-')) + "/export/3";
        }

        private void MergeCardData(Deck deck) {
            foreach (Card c in deck) {
                CardData cardData = rep.GetCardData(c.Title);
                c.SetCardData(cardData);
            }
        }

        private List<string> ParseLines(string deckHtml) {
            var lines = deckHtml.Split(Environment.NewLine.ToCharArray());
            var markDown = new List<string>();
            foreach (string line in lines) {
                if (IsValid(line)) {
                    markDown.Add(line);
                }
            }
            return markDown;
        }

        private Deck ParseDeck(IEnumerable<string> lines) {
            Deck deck = new Deck();
            foreach (string line in lines) {
                ParseLine(line, deck);
            }
            return deck;
        }

        private void ParseLine(string line, Deck deck) {
            if (line.StartsWith("*")) {
                ParseCard(line, deck);
            } else if (line.StartsWith("<textarea class")) {
                ParseTitle(line, deck);
            }
        }

        private void ParseTitle(string line, Deck deck) {
            deck.Title = line.Substring(line.IndexOf("###") + 3);
        }

        private void ParseCard(string line, Deck deck) {
            // * **1** x [Sylvanas Windrunner](http://www.hearthpwn.com/cards/33-sylvanas-windrunner)
            Card card = new Card();
            card.Count = int.Parse(line.Substring(4, 1));
            card.Title = WebUtility.HtmlDecode(line.Between("[","]"));
            card.Url = line.Between("(", ")");
            deck.AddCard(card);
        }

        private bool IsValid(string line) {
            return line.StartsWith("*") || line.Contains("###");
        }
    }
}
