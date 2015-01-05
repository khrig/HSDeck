using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HS
{
    public class CardArtImporter {
        public void Import(Deck deck) {
            Parallel.ForEach(deck, SetLocalImageUrl);
        }
        
        private void SetLocalImageUrl(Card card) {
            string fileName = Environment.CurrentDirectory + "\\" + card.Title + ".jpg";
            card.ImageUrl = fileName;
            if (File.Exists(fileName))
                return;

            Thread.Sleep(1000);

            using (WebClient client = new WebClient()) {
                using (MemoryStream ms = new MemoryStream(client.DownloadData(card.HttpImageUrl))) {
                    File.WriteAllBytes(fileName, ms.ToArray());
                }
            }
        }
    }
}
