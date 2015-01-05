using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace HS
{
    public class CardRepository {
        private JObject jo;
        public CardRepository() {
            UpdateDatabase();

            string json = File.ReadAllText(".\\AllSets.enUS.json");
            jo = JObject.Parse(json);
        }

        private void UpdateDatabase() {
            if (!File.Exists(".\\AllSets.enUS.json") || File.GetCreationTime(".\\AllSets.enUS.json").AddHours(10) < DateTime.Now)
                FetchDatabase();
        }

        private void FetchDatabase() {
            WebClient wc = new WebClient();
            wc.DownloadFile("http://hearthstonejson.com/json/AllSets.enUS.json", ".\\AllSets.enUS.json");
        }

        public CardData GetCardData(string name) {
            foreach (var set in jo) {
                foreach (var card in set.Value) {
                    if (String.Equals(((string)card["name"]), System.Net.WebUtility.HtmlDecode(name), StringComparison.CurrentCultureIgnoreCase) 
                        && card["type"].Value<string>() != "Enchantment")
                        return ToCard(card);
                }
            }
            return null;
        }

        private CardData ToCard(JToken card) {
            return new CardData {
                Cost = card["cost"] == null ? 0 : (int)card["cost"],
                ImageUrl = GetUrl(card["id"].Value<string>()),
                Title = card["name"].Value<string>()
            };
        }

        private string GetUrl(string value) {
            return "http://wow.zamimg.com/images/hearthstone/cards/enus/mediumj/" + value + ".jpg";
        }
    }
}


