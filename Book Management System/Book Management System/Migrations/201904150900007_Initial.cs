namespace Book_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Book", "ImageURL", c => c.String(nullable: false));
        }
    }
}
