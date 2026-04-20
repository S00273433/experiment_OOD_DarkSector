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
        public int ActionChoice;

        public event EventHandler CurrentCardChanged;

        public enum GameState { Begin, Describe, Decide, CombatStarted, Resolve }
        GameState CurrentGameState;

        private Card[] cardSequence;
        private int currentCardIndex = -1; //list so it can be accessed in mainwindow using NextCard, which replaced a part of PlayDeck from an earlier version

        public EventsTurns(Deck deck, Player player, Card currentCard, Combat combat)
        {
            deck.ShuffleCards();
            this.deck = deck;
            this.player = player;
            this.currentCard = currentCard;
            CurrentGameState = GameState.Begin;

            InitializeDeck();
        }

        public void InitializeDeck()
        {
            // Deal cards in sequence: 1 StartCard, 3 Act1, 3 Act2, 3 Act3, 1 Boss
            cardSequence = new Card[11];

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

            // Move to first card
            NextCard();
        }

        public void NextCard() //used to change the card displayed in mainwindow
        {
            currentCardIndex++;
            if (currentCardIndex < cardSequence.Length && cardSequence[currentCardIndex] != null)
            {
                currentCard = cardSequence[currentCardIndex];
                currentCard.IsActive = true;
                CurrentCardChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                currentCard = null;
                CurrentGameState = GameState.Begin;
            }
        }

        public void PathChoice()
        {
            string choice;
            Console.WriteLine("Choose a path");
        }

        public void LeaveCombat()
        {
            Console.WriteLine($"{currentCard.CardName} defeated");
            CurrentGameState = GameState.Begin;
            NextCard();
        }
    }
}
