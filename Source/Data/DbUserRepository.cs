using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Model;
using MySql.Data.MySqlClient;
using Presentation;

namespace Data
{
    public class DbUserRepository : IUserRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["LocalUserDatabase"].ConnectionString;

        public User GetUser(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                User user;

                try
                {
                    using (var context = new UserDbContext(connection, false))
                    {

                        context.Database.UseTransaction(transaction);

                        user = context.Users.SingleOrDefault(u => u.Id == id);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return user;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                IEnumerable<User> users;

                try
                {
                    using (var context = new UserDbContext(connection, false))
                    {

                        context.Database.UseTransaction(transaction);

                        users = context.Users.ToArray();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                return users;
            }
        }

        public void AddUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new UserDbContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);

                        context.Users.Add(user);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DeleteUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new UserDbContext(connection, false))
                    {

                        context.Database.UseTransaction(transaction);

                        context.Users.Attach(user);
                        context.Users.Remove(user);

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new UserDbContext(connection, false))
                    {

                        context.Database.UseTransaction(transaction);

                        context.Entry(user).State = EntityState.Modified;

                        SaveChanges(context);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private void SaveChanges(UserDbContext context)
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    // Update the values of the entity that failed to save from the store 
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed); 

        }
    }
}
