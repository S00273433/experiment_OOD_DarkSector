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
        private Player player;
        private Combat combat;

        public MainWindow()
        {
            InitializeComponent();
            Deck deck = new Deck();
            player = new Player();
            Card startCard = new Card();

            evTurns = new EventsTurns(deck, player, startCard, combat);

            // Subscribe to card changes
            evTurns.CurrentCardChanged += EvTurns_CurrentCardChanged;

            // Subscribe to health changes
            player.HealthChanged += Player_HealthChanged;
            evTurns.currentCard.StrengthChanged += EvTurns_CurrentCardStrengthChange;
            
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCardDisplay();
            UpdatePlayerHealthDisplay();
        }

        private void EvTurns_CurrentCardChanged(object sender, EventArgs e)
        {
            UpdateCardDisplay();
        }
        private void EvTurns_CurrentCardStrengthChange(object sender, EventArgs e)
        {
            Update_CardStrengthDisplay();
        }

        private void Player_HealthChanged(object sender, EventArgs e)
        {
            UpdatePlayerHealthDisplay();
        }

        private void UpdateCardDisplay()
        {
            txtStory.Text = evTurns.currentCard?.CardDescription;
            txtEnemyHealth.Text = evTurns.currentCard?.Strength.ToString(); //current cards health
        }
        private void Update_CardStrengthDisplay()
        {
            txtEnemyHealth.Text = evTurns.currentCard.Strength.ToString();
        }

        private void UpdatePlayerHealthDisplay()
        {
            txtPlayerHealth.Text = player.Health.ToString(); //player current health
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            OtherPressed = true;
            evTurns.NextCard();
        }

        private void btnCombat_Click(object sender, RoutedEventArgs e)
        {
           // txtStory.Text = combat.DoCombat(player, evTurns.currentCard,);
        }
    }
}
