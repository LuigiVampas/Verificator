using System.Data.Common;
using System.Data.Entity;
using Model;
using MySql.Data.Entity;

namespace Data
{
    [DbConfigurationType(typeof (MySqlEFConfiguration))]
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().MapToStoredProcedures();
            modelBuilder.Entity<User>().HasKey(user => user.Id);
            modelBuilder.Entity<User>().Property(user => user.Login).HasColumnName("Login");
            modelBuilder.Entity<User>().Property(user => user.Password).HasColumnName("Password");
            modelBuilder.Entity<User>().Property(user => user.Name).HasColumnName("Name");
            modelBuilder.Entity<User>().Property(user => user.Lastname).HasColumnName("Lastname");
            modelBuilder.Entity<User>().Property(user => user.Surname).HasColumnName("Surname");
            modelBuilder.Entity<User>().Property(user => user.Position).HasColumnName("Position");
        }
    }
}