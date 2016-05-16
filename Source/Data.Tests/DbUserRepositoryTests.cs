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

        [Test]
        public void GetUserFromRepository()
        {
            var repository = new DbUserRepository();

            repository.AddUser(_user);

            var users = repository.GetAllUsers();

            var userid = users.First().Id;

            var userWithNecessaryId = repository.GetUser(userid);

            Assert.True(users.First().Equals(userWithNecessaryId));

        }

        [Test]
        public void UpdateUserInRepository()
        {
            var repository = new DbUserRepository();

            repository.AddUser(_user);
            
            var users = repository.GetAllUsers();
            
            Assert.True(_user.Equals(users.First()));

            var newUser = _user;

            newUser.Name = "Semen";

            repository.UpdateUser(newUser);

            var newUsers = repository.GetAllUsers();

            Assert.True(newUser.Equals(newUsers.First()));

        }
    }
}
    
