namespace CQX.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Controller = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Action = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Controller = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Action = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Short(nullable: false),
                        ResourceId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        Password = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        RealName = c.String(maxLength: 20, storeType: "nvarchar"),
                        MobileNumber = c.String(maxLength: 20, storeType: "nvarchar"),
                        Email = c.String(maxLength: 20, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 100, storeType: "nvarchar"),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserGroups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        Permission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.Permission_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Permission_Id);
            
            CreateTable(
                "dbo.UserGroupRoles",
                c => new
                    {
                        UserGroup_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_Id, t.Role_Id })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.UserGroup_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Group_Id", "dbo.UserGroups");
            DropForeignKey("dbo.UserGroupRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserGroupRoles", "UserGroup_Id", "dbo.UserGroups");
            DropForeignKey("dbo.RolePermissions", "Permission_Id", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Pages", "Group_Id", "dbo.Modules");
            DropIndex("dbo.UserGroupRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserGroupRoles", new[] { "UserGroup_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Permission_Id" });
            DropIndex("dbo.RolePermissions", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Group_Id" });
            DropIndex("dbo.Pages", new[] { "Group_Id" });
            DropTable("dbo.UserGroupRoles");
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Users");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Pages");
            DropTable("dbo.Modules");
        }
    }
}
