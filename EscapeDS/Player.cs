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
        public bool InCombat { get; set; }
        
        public int Health
        {
            set
            {
                int Baseealth = 12;
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
            get { return health; }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }




        public Player()
        { 
         
        }
        public bool DoBattle(Card competitingCard)
        {   
            return true;
        }
        
    }
    public class Inventory
    {
        public List<Inventory> Items {  get; set; }
        
        public Inventory()
        {
            Items = new List<Inventory>();
            Items.Add(new Inventory());
        }
    }

    

}
