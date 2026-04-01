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
            Player player = new Player();
            EventsTurns evTurns = new EventsTurns(deck, player, card);
            Combat combat = new Combat(card, player, evTurns);

            card = card.CardID == 1 ? card : deck.DealCard(CardType.Act1);
            card = card.CardID == 2 ? card : deck.DealCard(CardType.Act1);
            card = card.CardID == 3 ? card : deck.DealCard(CardType.Act1);
            card = card.CardID == 4 ? card : deck.DealCard(CardType.Act1);
            card = card.CardID == 5 ? card : deck.DealCard(CardType.Act1);
            
            

           

            Console.WriteLine(card);

            

            combat.DoCombat(player, card, "Attack");


        }
    }
}
