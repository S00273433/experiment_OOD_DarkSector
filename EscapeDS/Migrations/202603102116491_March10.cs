namespace EscapeDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class March10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "CardDescription", c => c.String());
            AddColumn("dbo.Cards", "TypeOfCard", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "CardReward", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "CardPenalty", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "BossID", c => c.Int());
            AddColumn("dbo.Cards", "BossName", c => c.String());
            AddColumn("dbo.Cards", "BossImage", c => c.String());
            AddColumn("dbo.Cards", "StartID", c => c.Int());
            AddColumn("dbo.Cards", "StartName", c => c.String());
            AddColumn("dbo.Cards", "StartImage", c => c.String());
            AddColumn("dbo.Cards", "StartReward", c => c.String());
            AddColumn("dbo.Cards", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "Discriminator");
            DropColumn("dbo.Cards", "StartReward");
            DropColumn("dbo.Cards", "StartImage");
            DropColumn("dbo.Cards", "StartName");
            DropColumn("dbo.Cards", "StartID");
            DropColumn("dbo.Cards", "BossImage");
            DropColumn("dbo.Cards", "BossName");
            DropColumn("dbo.Cards", "BossID");
            DropColumn("dbo.Cards", "CardPenalty");
            DropColumn("dbo.Cards", "CardReward");
            DropColumn("dbo.Cards", "TypeOfCard");
            DropColumn("dbo.Cards", "CardDescription");
        }
    }
}
