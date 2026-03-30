namespace EscapeDS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards1",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        CardName = c.String(),
                        CardDescription = c.String(),
                        Image = c.String(),
                        TypeOfCard = c.Int(nullable: false),
                        RangeDamage = c.Int(nullable: false),
                        MeleeDamage = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        CardReward = c.Int(nullable: false),
                        CardPenalty = c.Int(nullable: false),
                        BossID = c.Int(),
                        BossName = c.String(),
                        BossImage = c.String(),
                        StartID = c.Int(),
                        StartName = c.String(),
                        StartImage = c.String(),
                        StartReward = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CardID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards1");
        }
    }
}
