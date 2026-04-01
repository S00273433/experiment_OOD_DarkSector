using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EscapeDS
{
    public class EventsTurns
    {
        public Deck deck;
        public Player player;
        public Card currentCard;
        private Combat Combat;

        public enum GameState { Begin, Describe, Decide, CombatStarted, Resolve }
        GameState CurrentGameState;
        
        
        public EventsTurns(Deck deck, Player player, Card currentCard)
        {
            deck.ShuffleCards();
            this.deck = deck;
            this.player = player;
            this.currentCard = currentCard;
            TurnOrder();
        }

        public void TurnOrder()
        {
            if(CurrentGameState == GameState.Begin)
            {
                Console.WriteLine($"{currentCard.CardDescription}");
            }

        }



        public void LeaveCombat()
        {
            Console.WriteLine($"{ currentCard.CardName} defeated");
            CurrentGameState = GameState.Begin;
        }

        

    }
}
