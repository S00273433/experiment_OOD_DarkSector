using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDS
{
    public class Combat 
    {
        //class that runs the combat encounters in the game. pulls stats from whatever card is currently active in the databse. 
        
        Card Card;
        Player Player;

        int Strength;
        private int CurrentCard { get; set; }
        
        public Combat(int cardID)
        {
            cardID = this.Card.CardID;
            CurrentCard = cardID;
            Strength = this.Card.Strength;
            

            //Compare the card's strentgh against the payer's dice roll every turn.
            //when strength is reduced to 0, the player wins and gets the reward.

            //Then, roll car's hit dice. if hit, reduce player health. 0 = game over.
        }

    }
}
