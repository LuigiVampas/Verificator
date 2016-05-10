using System.Data.Common;
using System.Data.Entity;
using Model;
using MySql.Data.Entity;

namespace Data
{
    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof (MySqlEFConfiguration))]
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext()
            : base()
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public UserDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().MapToStoredProcedures();
        }
    }
}