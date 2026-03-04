using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EscapeDS;

namespace DatabaseManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CardData db = new CardData();
            using (db)
            {
                Card card = new Card() { CardName = "TestCard", Image = "1.jpg" };
                db.Card.Add(card);
                db.SaveChanges();
            }
        }
    }
}
