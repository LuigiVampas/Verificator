using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Data.Tests
{
    public class DbUserRepositoryTests
    {
        [Test]
        public void AddUserToDb()
        {
            const string connectionString = "server=127.0.0.1;port=2096;database=test;uid=root;password=root";

            List<User> users = new List<User>();

            users.Add(new User
            {
                Lastname = "Sokov",
                Login = "cwdlcs",
                Name = "Andrey",
                Position = "Engineer",
                Surname = "Igorevich",
                Password = "Qwerty123!asd"
            });


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (var contextDb = new UserDbContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (var context = new UserDbContext(connection, false))
                    {

                        // Passing an existing transaction to the context
                        context.Database.UseTransaction(transaction);

                        // DbSet.AddRange
                        
                        context.Users.AddRange(users);

                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

                transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (var context = new UserDbContext(connection, false))
                    {

                        Assert.That(context.Users.Contains(users.First()), Is.True);
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
    }
}
    
