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
            CurrentGameState = GameState.Begin;
            PlayDeck();
        }

        public void PlayDeck()
        {
            // Deal cards in sequence: 1 StartCard, 3 Act1, 3 Act2, 3 Act3, 1 Boss
            Card[] cardSequence = new Card[11];
            
            // Deal 1 StartCard
            cardSequence[0] = deck.DealCard(CardType.StartCard);
            
            // Deal 3 Act1 cards
            cardSequence[1] = deck.DealCard(CardType.Act1);
            cardSequence[2] = deck.DealCard(CardType.Act1);
            cardSequence[3] = deck.DealCard(CardType.Act1);
            
            // Deal 3 Act2 cards
            cardSequence[4] = deck.DealCard(CardType.Act2);
            cardSequence[5] = deck.DealCard(CardType.Act2);
            cardSequence[6] = deck.DealCard(CardType.Act2);
            
            // Deal 3 Act3 cards
            cardSequence[7] = deck.DealCard(CardType.Act3);
            cardSequence[8] = deck.DealCard(CardType.Act3);
            cardSequence[9] = deck.DealCard(CardType.Act3);
            
            // Deal 1 Boss card
            cardSequence[10] = deck.DealCard(CardType.Boss);

            // Loop through each dealt card
            foreach (Card card in cardSequence)
            {
                if (card != null)
                {
                    currentCard = card;
                    currentCard.IsActive = true;

                    // Display the card while IsActive is true
                    while (currentCard.IsActive)
                    {
                        Console.Clear();
                        Console.WriteLine($"{currentCard.CardName}");
                        Console.WriteLine("=====================");
                        Console.WriteLine($"{currentCard.CardDescription}");
                        Console.WriteLine("\n=====================");
                        Console.WriteLine("Type 'Next' to proceed to the next card:");
                        
                        string input = Console.ReadLine().ToLower();
                        
                        if (input == "next")
                        {
                            currentCard.IsActive = false;
                        }
                    }
                }
            }

            Console.WriteLine("All cards have been dealt. Game Over.");
        }

        public void LeaveCombat()
        {
            Console.WriteLine($"{ currentCard.CardName} defeated");
            CurrentGameState = GameState.Begin;
        }

        

    }
}
