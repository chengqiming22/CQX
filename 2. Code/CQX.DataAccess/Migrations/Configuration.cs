namespace CQX.DataAccess.Migrations
{
    using CQX.DataModel.Entites;
    using CQX.DataModel.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CQXDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CQXDbContext context)
        {
            //添加页面
            context.Pages.AddOrUpdate(p => p.Name,
                new Page { Name = "应用列表", Controller = "App", Action = "Index" },
                new Page { Name = "关于我们", Controller = "About", Action = "Index" });

            context.Modules.AddOrUpdate(g => g.Name,
                new Module { Name = "应用管理", Controller = "App", Action = "Index" },
                new Module { Name = "关于我们", Controller = "About", Action = "Index" });

            context.UserGroups.AddOrUpdate(u => u.Name,
                new UserGroup { Name = "admin", Remark = "超级管理员" });

            context.Users.AddOrUpdate(u => u.UserName,
                new User { UserName = "admin", Password = "AMz+TNtYabPVsjjLdeZ12squCMZubtjeUKt7TJmOjFay6XIqK+3lZwHFE42IC6o74w==", IsActive = true });

            context.Roles.AddOrUpdate(r => r.Name,
                new Role { Name = "系统管理员" });

            context.SaveChanges();

            //页面与模块关联
            context.Modules.Include("Pages").First(g => g.Name == "应用管理").Pages = context.Pages.Where(p => p.Controller == "App").ToList();
            context.Modules.Include("Pages").First(g => g.Name == "关于我们").Pages = context.Pages.Where(p => p.Controller == "About").ToList();

            //添加页面权限
            context.Permissions.AddOrUpdate(p => p.ResourceId,
                new Permission { Type = (short)PermissionType.Page, ResourceId = context.Pages.First(c => c.Name == "应用列表").Id, IsActive = true },
                new Permission { Type = (short)PermissionType.Page, ResourceId = context.Pages.First(c => c.Name == "关于我们").Id, IsActive = true });

            context.SaveChanges();

            context.Roles.Include("Permissions").First(r => r.Name == "系统管理员").Permissions = context.Permissions.ToList();

            context.UserGroups.Include("Roles").First(u => u.Name == "admin").Roles = context.Roles.ToList();
            context.UserGroups.Include("Users").First(u => u.Name == "admin").Users = context.Users.ToList();

            context.SaveChanges();
        }
    }
}
