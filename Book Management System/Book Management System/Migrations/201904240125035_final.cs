namespace Book_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 200),
                        IdRole = c.String(nullable: false, maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.IdRole)
                .Index(t => t.IdRole);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Total = c.Double(nullable: false),
                        IsActive = c.Boolean(),
                        IdUser = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.CartItem",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Quantity = c.Int(nullable: false),
                        IdBook = c.String(nullable: false, maxLength: 10),
                        IdCard = c.String(nullable: false, maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.IdBook)
                .ForeignKey("dbo.Cart", t => t.IdCard)
                .Index(t => t.IdBook)
                .Index(t => t.IdCard);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Title = c.String(nullable: false, maxLength: 200),
                        EnTitle = c.String(nullable: false, maxLength: 200),
                        Summary = c.String(),
                        EnSummary = c.String(nullable: false),
                        ImageURL = c.String(),
                        ISBN = c.String(nullable: false, maxLength: 100),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        IdAuthor = c.String(nullable: false, maxLength: 10),
                        IdPublisher = c.String(nullable: false, maxLength: 10),
                        IdCategory = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.IdAuthor)
                .ForeignKey("dbo.BookCategory", t => t.IdCategory)
                .ForeignKey("dbo.Publisher", t => t.IdPublisher)
                .Index(t => t.IdAuthor)
                .Index(t => t.IdPublisher)
                .Index(t => t.IdCategory);
            
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookCategory",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        CateName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Content = c.String(nullable: false),
                        CommenterName = c.String(nullable: false, maxLength: 10),
                        IdBook = c.String(nullable: false, maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Book", t => t.IdBook)
                .Index(t => t.IdBook);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        IdCard = c.String(nullable: false, maxLength: 10),
                        CreateDate = c.DateTime(nullable: false),
                        MethodPayment = c.String(nullable: false, maxLength: 100),
                        Status = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cart", t => t.IdCard)
                .Index(t => t.IdCard);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        RoleName = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Birthday = c.DateTime(),
                        Email = c.String(maxLength: 100),
                        Address = c.String(maxLength: 100),
                        Gender = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Avatar = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Account", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Id", "dbo.Account");
            DropForeignKey("dbo.Account", "IdRole", "dbo.Role");
            DropForeignKey("dbo.Cart", "IdUser", "dbo.Account");
            DropForeignKey("dbo.Order", "IdCard", "dbo.Cart");
            DropForeignKey("dbo.CartItem", "IdCard", "dbo.Cart");
            DropForeignKey("dbo.Book", "IdPublisher", "dbo.Publisher");
            DropForeignKey("dbo.Comment", "IdBook", "dbo.Book");
            DropForeignKey("dbo.CartItem", "IdBook", "dbo.Book");
            DropForeignKey("dbo.Book", "IdCategory", "dbo.BookCategory");
            DropForeignKey("dbo.Book", "IdAuthor", "dbo.Author");
            DropIndex("dbo.User", new[] { "Id" });
            DropIndex("dbo.Order", new[] { "IdCard" });
            DropIndex("dbo.Comment", new[] { "IdBook" });
            DropIndex("dbo.Book", new[] { "IdCategory" });
            DropIndex("dbo.Book", new[] { "IdPublisher" });
            DropIndex("dbo.Book", new[] { "IdAuthor" });
            DropIndex("dbo.CartItem", new[] { "IdCard" });
            DropIndex("dbo.CartItem", new[] { "IdBook" });
            DropIndex("dbo.Cart", new[] { "IdUser" });
            DropIndex("dbo.Account", new[] { "IdRole" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Order");
            DropTable("dbo.Publisher");
            DropTable("dbo.Comment");
            DropTable("dbo.BookCategory");
            DropTable("dbo.Author");
            DropTable("dbo.Book");
            DropTable("dbo.CartItem");
            DropTable("dbo.Cart");
            DropTable("dbo.Account");
        }
    }
}
