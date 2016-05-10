using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;
using Model;

namespace Data
{
    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof (MySqlEFConfiguration))]
    public class UserDbContex : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContex()
            : base()
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public UserDbContex(DbConnection existingConnection, bool contextOwnsConnection)
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