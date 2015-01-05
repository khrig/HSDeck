using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace HS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private HearthPwnImporter importer;
        private Deck deck;

        public MainWindow()
        {
            InitializeComponent();

            importer = new HearthPwnImporter();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            await Task.Factory.StartNew(ImportDeck);
            ListDeck();
        }

        private void ImportDeck() {
            string deckUrl = "http://www.hearthpwn.com/decks/139450-1legend-eu-neptulon-shaman";
            //string deckUrl = "http://www.hearthpwn.com/decks/138004-sjow-warrior-god";

            deck = importer.Import(deckUrl);
            deck.SortByCostAsc();
        }

        private void ListDeck() {
            DataContext = deck;
            /*
            lstBox.Items.Clear();
            foreach (var c in deck) {
                lstBox.Items.Add(c);
            }*/
        }
    }
}
