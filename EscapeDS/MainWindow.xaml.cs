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
            evTurns = new EventsTurns(deck, player, startCard, null);
            combat = new Combat(evTurns.currentCard, player, evTurns);

            // Subscribe to card changes
            evTurns.CurrentCardChanged += EvTurns_CurrentCardChanged;

            // Subscribe to health changes
            player.HealthChanged += Player_HealthChanged;
            evTurns.currentCard.StrengthChanged += EvTurns_CurrentCardStrengthChange;

            // Subscribe to combat log updates
            combat.CombatLogUpdated += Combat_CombatLogUpdated;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCardDisplay();
            UpdatePlayerHealthDisplay();
        }

        private void EvTurns_CurrentCardChanged(object sender, EventArgs e)
        {
            UpdateCardDisplay();
            HideCombatButtons();
            ClearCombatLog();
        }

        private void EvTurns_CurrentCardStrengthChange(object sender, EventArgs e)
        {
            Update_CardStrengthDisplay();
        }

        private void Player_HealthChanged(object sender, EventArgs e)
        {
            UpdatePlayerHealthDisplay();
        }

        private void Combat_CombatLogUpdated(object sender, Combat.CombatLogEventArgs e)
        {
            AppendToCombatLog(e.Message);
        }

        private void UpdateCardDisplay()
        {
            txtStory.Text = evTurns.currentCard?.CardDescription;
            //imgCard.Source = evTurns.currentCard.Image;
            txtEnemyHealth.Text = evTurns.currentCard?.Strength.ToString();
        }

        private void Update_CardStrengthDisplay()
        {
            txtEnemyHealth.Text = evTurns.currentCard.Strength.ToString();
        }

        private void UpdatePlayerHealthDisplay()
        {
            txtPlayerHealth.Text = player.Health.ToString();
        }

        private void ClearCombatLog()
        {
            txtCombatInfo.Text = string.Empty;
        }

        private void AppendToCombatLog(string message)
        {
            txtCombatInfo.Text += message + Environment.NewLine;
            // Auto-scroll to bottom
            scrollCombatInfo.ScrollToBottom();
        }

        private void HideCombatButtons()
        {
            btnPistol.Visibility = Visibility.Hidden;
            btnRifle.Visibility = Visibility.Hidden;
            btnReload.Visibility = Visibility.Hidden;
        }

        private void btnOther_Click(object sender, RoutedEventArgs e)
        {
            OtherPressed = true;
            evTurns.NextCard();
        }

        private void btnCombat_Click(object sender, RoutedEventArgs e)
        {
            this.combat.StartCombat(evTurns.currentCard);
            btnPistol.Visibility = Visibility.Visible;
            btnRifle.Visibility = Visibility.Visible;
            btnReload.Visibility = Visibility.Visible;
            btnHeal.Visibility = Visibility.Visible;
        }

        private void btnPistol_Click(object sender, RoutedEventArgs e)
        {
            this.combat.ProcessWeaponChoice("1");
            this.combat.ProcessActionChoice("1");
        }

        private void btnRifle_Click(object sender, RoutedEventArgs e)
        {
            this.combat.ProcessWeaponChoice("2");
            this.combat.ProcessActionChoice("1");
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            this.combat.ProcessActionChoice("3");
        }

        private void btnHeal_Click(object sender, RoutedEventArgs e)
        {
            this.combat.ProcessActionChoice("2");
        }
    }
}
