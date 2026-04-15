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
            Card card = new Card();
            Player player = new Player();
            EventsTurns evTurns = new EventsTurns(deck, player, card);
            Combat combat = new Combat(card, player, evTurns);


            
           /* foreach (var c in deck.ShuffledCards)
            {
                Console.WriteLine($"Card Name: {c.CardName}, Card Type: {c.CardDescription}");
            }*/
                



            combat.DoCombat(player, card, "Attack");


        }
    }
}
