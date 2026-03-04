using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDS
{
    public class Card
    {
        //chapter cards for DarkSector

        public int CardID { get; set; }
        public string CardName { get; set; }
        public string Image { get; set; }

        public int RangeDamage;
        public int MeleeDamage; //may cut out
        private int strength;

        public int Strength
        {
            set
            {
                //remake this when doing combat
                int baseStrength = 0;

                Random rand = new Random();
                baseStrength += rand.Next(1, 6); // Random strength between 1 and 5

                strength = baseStrength; // Set the strength property

                
            }

            get 
            { 
               return strength;

            }
            
        }




    }


 


    public class CardData : DbContext
    {
        public CardData() : base("CardDataFeb25") { }
        public DbSet<Card> Card { get; set; }
    }
}

