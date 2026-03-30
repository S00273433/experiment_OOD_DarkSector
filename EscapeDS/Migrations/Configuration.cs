namespace EscapeDS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<EscapeDS.CardData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EscapeDS.CardData context)
        {
            /*var deck = new EscapeDS.Deck();

            // Add all cards from the deck to the database
            foreach (var card in deck.AllCards)
            {
                context.Card.AddOrUpdate(c => c.CardID, card);
            }*/
        }
    }
}
