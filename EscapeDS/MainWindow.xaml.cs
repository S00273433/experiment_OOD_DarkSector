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
        public Deck deck = new Deck();
        public Card card = new Card();
        public Player player = new Player();
        public EventsTurns evTurns;
        public Combat combat;

        public bool OtherPressed;
        
        List<Card> allCards = new List<Card>();


        public MainWindow()
        {
            InitializeComponent(); 

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            evTurns = new EventsTurns(deck, player, card);

            txtStory.Text = evTurns.currentCard.CardDescription;
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
                OtherPressed = true;
                
                txtStory.Text = evTurns.currentCard?.CardDescription;
                
                
                
        }
    }
}
