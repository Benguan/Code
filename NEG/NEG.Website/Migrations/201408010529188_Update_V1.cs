namespace NEG.Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_V1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.APIDetailInfoes", "APIKey", c => c.String(maxLength: 20));
            AddColumn("dbo.ModuleDetailInfoes", "ModuleKey", c => c.String(maxLength: 20));

        }
        
        public override void Down()
        {
            DropColumn("dbo.ModuleDetailInfoes", "ModuleKey");
            DropColumn("dbo.APIDetailInfoes", "APIKey");
        }
    }
}
