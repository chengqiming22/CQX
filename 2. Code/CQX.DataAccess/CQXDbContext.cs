namespace CQX.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using MySql.Data.Entity;
    using CQX.DataModel.Entites;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class CQXDbContext : DbContext
    {
        public CQXDbContext()
            : base("name=cqx_db")
        {

        }

        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Page> Pages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasMany(r => r.Permissions).WithMany();
            modelBuilder.Entity<UserGroup>().HasMany(u => u.Roles).WithMany();
            modelBuilder.Entity<UserGroup>().HasMany(u => u.Users).WithOptional(g => g.Group);
            modelBuilder.Entity<Module>().HasMany(g => g.Pages).WithOptional(p => p.Group).WillCascadeOnDelete();
        }
    }
}
