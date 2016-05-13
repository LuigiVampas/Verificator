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

        public UserDbContext()
        {

        }

        public UserDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().MapToStoredProcedures();
          //  modelBuilder.Entity<User>().HasRequired(u => u.Login)
                                       
    
        }
    }
}