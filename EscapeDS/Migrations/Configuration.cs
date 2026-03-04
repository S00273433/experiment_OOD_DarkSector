namespace EscapeDS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EscapeDS.CardData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EscapeDS.CardData";
        }

        protected override void Seed(EscapeDS.CardData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
