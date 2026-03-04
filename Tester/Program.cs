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

            Console.WriteLine(card);


        }
    }
}
