using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDS
{
    public class EventsTurns
    {
        public Deck deck;
        public Player player;
        public Card currentCard;


        public EventsTurns(Deck deck, Player player, Card currentCard)
        {
            deck.ShuffleCards();
            this.deck = deck;
            this.player = player;
            this.currentCard = currentCard;
        }

        

    }
}
