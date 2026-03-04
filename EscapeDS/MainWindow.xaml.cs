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

        List<Card> allCards = new List<Card>();

        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CardData db = new CardData();

            var query = from c in db.Card
                        orderby c.CardID
                        select c;

            allCards = query.ToList();



            Player player = new Player();
            Card card1 = new Card() { CardName = "Card1", Image = "1.jpg" };

                bool result = player.DoBattle(card1);
    
                MessageBox.Show(result.ToString());


        }
    }
}
