namespace Book_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MethodPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "MethodPayment", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "MethodPayment");
        }
    }
}
