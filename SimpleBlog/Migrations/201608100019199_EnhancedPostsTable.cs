namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnhancedPostsTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TagPosts", newName: "PostTags");
            DropIndex("dbo.Posts", new[] { "User_Id" });
            RenameColumn(table: "dbo.PostTags", name: "Tag_Id", newName: "TagId");
            RenameColumn(table: "dbo.PostTags", name: "Post_Id", newName: "PostId");
            RenameColumn(table: "dbo.Posts", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.PostTags", name: "IX_Post_Id", newName: "IX_PostId");
            RenameIndex(table: "dbo.PostTags", name: "IX_Tag_Id", newName: "IX_TagId");
            DropPrimaryKey("dbo.PostTags");
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Posts", "Slug", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false, maxLength: 2000));
            AlterColumn("dbo.Posts", "UserId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostTags", new[] { "PostId", "TagId" });
            CreateIndex("dbo.Posts", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropPrimaryKey("dbo.PostTags");
            AlterColumn("dbo.Posts", "UserId", c => c.Int());
            AlterColumn("dbo.Posts", "Content", c => c.String());
            AlterColumn("dbo.Posts", "Slug", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AddPrimaryKey("dbo.PostTags", new[] { "Tag_Id", "Post_Id" });
            RenameIndex(table: "dbo.PostTags", name: "IX_TagId", newName: "IX_Tag_Id");
            RenameIndex(table: "dbo.PostTags", name: "IX_PostId", newName: "IX_Post_Id");
            RenameColumn(table: "dbo.Posts", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.PostTags", name: "PostId", newName: "Post_Id");
            RenameColumn(table: "dbo.PostTags", name: "TagId", newName: "Tag_Id");
            CreateIndex("dbo.Posts", "User_Id");
            RenameTable(name: "dbo.PostTags", newName: "TagPosts");
        }
    }
}
