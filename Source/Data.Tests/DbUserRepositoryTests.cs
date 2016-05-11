using System.Linq;
using Model;
using MySql.Data.MySqlClient;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class DbUserRepositoryTests
    {
        private User _user;

        private const string ConnectionString = "server=127.0.0.1;port=3306;database=Users;uid=root";

        [SetUp]
        public void SetUp()
        {
            _user = new User
            {
                Lastname = "Sokov",
                Login = "cwdlcs",
                Name = "Andrey",
                Position = "Engineer",
                Surname = "Igorevich",
                Password = "Qwerty123!asd"
            };
        }

        [Test]
        public void AddUserToDb()
        {

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                // Create database if not exists
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

                        context.Users.Add(_user);

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
                    using (var context = new UserDbContext(connection, false))
                    {
                        var userInDb = context.Users.Where(u => u.Id == _user.Id).First();
                        Assert.That(userInDb == _user);
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

        [Test]
        public void AddUserToRepository()
        {
            var repository = new DbUserRepository();

            repository.AddUser(_user);

            var users = repository.GetAllUsers();

            var userInDb = users.FirstOrDefault(u => u == _user);

            Assert.That(userInDb, Is.Not.Null);

            var sameUsers = repository.GetAllUsers().Where(u => u == _user);

            foreach (var userClone in sameUsers)
                repository.DeleteUser(userClone);

            users = repository.GetAllUsers();

            Assert.That(users.All(u => u != _user), Is.True);
        }
    }
}
    
