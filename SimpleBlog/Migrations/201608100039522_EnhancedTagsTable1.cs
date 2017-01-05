namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnhancedTagsTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Slug", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Name", c => c.String());
            AlterColumn("dbo.Tags", "Slug", c => c.String());
        }
    }
}
