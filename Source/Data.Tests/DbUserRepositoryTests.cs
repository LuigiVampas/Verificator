using System.Linq;
using Model;
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

            var users = repository.GetAllUsers().ToArray();

            var userid = users.First().Id;

            var userWithNecessaryId = repository.GetUser(userid);

            Assert.True(users.First().Equals(userWithNecessaryId));
        }

        [Test]
        public void UpdateUserInRepository()
        {
            var repository = new DbUserRepository();

            var users = repository.GetAllUsers();

            foreach (var user in users)
                repository.DeleteUser(user);

            repository.AddUser(_user);
            
            users = repository.GetAllUsers();
            
            Assert.True(_user.Equals(users.First()));

            var newUser = _user;

            newUser.Name = "Semen";

            repository.UpdateUser(newUser);

            var newUsers = repository.GetAllUsers();

            Assert.True(newUser.Equals(newUsers.First()));

        }

        [Test]
        public void DeleteUserFromRepository()
        {
            var repository = new DbUserRepository();

            repository.AddUser(_user);

            var anotherUser = new User
            {
                Login = "Login",
                Password = "dsfhdskfhdsk",
                Name = "Name",
                Surname = "Surname",
                Initials = "Initials"
            };

            repository.AddUser(anotherUser);

            var users = repository.GetAllUsers();

            repository.DeleteUser(anotherUser);

            var newUsers = repository.GetAllUsers().ToArray();

            Assert.True(newUsers.Contains(_user));
            Assert.False(newUsers.Contains(anotherUser));
        }
    }
}
    
