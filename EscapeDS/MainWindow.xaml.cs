using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EscapeDS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public EventsTurns evTurns;
        public bool OtherPressed;

        public MainWindow()
        {
            InitializeComponent();
            Deck deck = new Deck();
            Player player = new Player();
            Card startCard = new Card();
            evTurns = new EventsTurns(deck, player, startCard);

            // Subscribe to card changes
            evTurns.CurrentCardChanged += EvTurns_CurrentCardChanged;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCardDisplay();
        }

        private void EvTurns_CurrentCardChanged(object sender, EventArgs e)
        {
            UpdateCardDisplay();
        }

        private void UpdateCardDisplay()
        {
            txtStory.Text = evTurns.currentCard?.CardDescription ?? "No card available";
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            OtherPressed = true;
            evTurns.NextCard();
        }
    }
}
