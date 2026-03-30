namespace DatabaseManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseManagement.Program>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected void Seed(EscapeDS.CardData context)
        {
            /*// Create a Deck instance to get all cards
            var deck = new EscapeDS.Deck();
            
            // Add all cards from the deck to the database
            foreach (var card in deck.AllCards)
            {
                context.Card.AddOrUpdate(c => c.CardID, card);
            }*/
            
            context.SaveChanges();
        }
    }
}
