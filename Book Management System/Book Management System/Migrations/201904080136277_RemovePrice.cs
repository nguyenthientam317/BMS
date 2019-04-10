namespace Book_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CartItem", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItem", "Price", c => c.Double(nullable: false));
        }
    }
}
