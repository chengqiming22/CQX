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
            //���ҳ��
            context.Pages.AddOrUpdate(p => p.Name,
                new Page { Name = "Ӧ���б�", Controller = "App", Action = "Index" },
                new Page { Name = "��������", Controller = "About", Action = "Index" });

            context.Modules.AddOrUpdate(g => g.Name,
                new Module { Name = "Ӧ�ù���", Controller = "App", Action = "Index" },
                new Module { Name = "��������", Controller = "About", Action = "Index" });

            context.UserGroups.AddOrUpdate(u => u.Name,
                new UserGroup { Name = "admin", Remark = "��������Ա" });

            context.Users.AddOrUpdate(u => u.UserName,
                new User { UserName = "admin", Password = "AMz+TNtYabPVsjjLdeZ12squCMZubtjeUKt7TJmOjFay6XIqK+3lZwHFE42IC6o74w==", IsActive = true });

            context.Roles.AddOrUpdate(r => r.Name,
                new Role { Name = "ϵͳ����Ա" });

            context.SaveChanges();

            //ҳ����ģ�����
            context.Modules.Include("Pages").First(g => g.Name == "Ӧ�ù���").Pages = context.Pages.Where(p => p.Controller == "App").ToList();
            context.Modules.Include("Pages").First(g => g.Name == "��������").Pages = context.Pages.Where(p => p.Controller == "About").ToList();

            //���ҳ��Ȩ��
            context.Permissions.AddOrUpdate(p => p.ResourceId,
                new Permission { Type = (short)PermissionType.Page, ResourceId = context.Pages.First(c => c.Name == "Ӧ���б�").Id, IsActive = true },
                new Permission { Type = (short)PermissionType.Page, ResourceId = context.Pages.First(c => c.Name == "��������").Id, IsActive = true });

            context.SaveChanges();

            context.Roles.Include("Permissions").First(r => r.Name == "ϵͳ����Ա").Permissions = context.Permissions.ToList();

            context.UserGroups.Include("Roles").First(u => u.Name == "admin").Roles = context.Roles.ToList();
            context.UserGroups.Include("Users").First(u => u.Name == "admin").Users = context.Users.ToList();

            context.SaveChanges();
        }
    }
}
