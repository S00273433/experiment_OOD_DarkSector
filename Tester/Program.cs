using EscapeDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Card card = deck.DealCard(CardType.Act1);
            Combat combat = new Combat(card);
            Player player = new Player();
           

           

            Console.WriteLine(card);

            

            combat.DoCombat(player, card, "Attack");


        }
    }
}
