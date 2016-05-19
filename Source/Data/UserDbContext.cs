using System.Data.Common;
using System.Data.Entity;
using Model;
using MySql.Data.Entity;

namespace Data
{
    /// <summary>
    /// Создание UserDbContext и описание структуры базы данных.
    /// </summary>
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
            modelBuilder.Entity<User>().Property(user => user.Login).HasColumnName("Login").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(user => user.Password).HasColumnName("Password").IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Name).HasColumnName("Name").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(user => user.Surname).HasColumnName("Surname").IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(user => user.Lastname).HasColumnName("Lastname").IsOptional().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(user => user.Initials).HasColumnName("Initials").IsOptional();
            modelBuilder.Entity<User>().Property(user => user.Position).HasColumnName("Position").IsOptional().HasMaxLength(20);
        }
    }
}