using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDS
{
    public class Player
    {
        private int health; 

        public int Health
        {
            set
            {
                int BaseHealth = 12;
                if (value < 0)
                {
                    health = 0; // Ensure health does not go below 0
                }
                else if (value > 12)
                {
                    health = 12; // Ensure health does not exceed 12
                }
                else
                {
                    health = value;
                }
            } 
            get { return Health; }
        }




        public Player()
        { 
         
        }
        public bool DoBattle(Card competitingCard)
        {
            return true;
        }
    }
}
