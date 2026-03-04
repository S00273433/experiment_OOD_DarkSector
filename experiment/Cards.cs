using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace experiment
{
    public class Card
    {
        //chapter cards for DarkSector

        public int CardID { get; set; }
        public string CardName { get; set; }
        public string Image { get; set; }

    }

    public class CardData : DbContext
    {
        public DbSet<Card> Card { get; set; }
    }
}

