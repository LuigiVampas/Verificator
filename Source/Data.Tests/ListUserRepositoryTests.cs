using System.Linq;
using Model;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    class ListUserRepositoryTests
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
        public void AddUserToList()
        {
            var usersList = new ListUserRepository();

            usersList.AddUser(_user);

            var usersFromList = usersList.GetAllUsers();
            Assert.True(_user.Equals(usersFromList.First()));

            usersList.AddUser(_user);
            Assert.That(usersList.GetAllUsers().Count(), Is.EqualTo(1));
            
            usersList.DeleteUser(_user);
            usersList.DeleteUser(_user);
            Assert.That(usersList.GetAllUsers(), Is.Empty);

        }

        [Test]
        public void DeleteUserFromList()
        {
            var usersList = new ListUserRepository();

            usersList.AddUser(_user);

            usersList.DeleteUser(_user);

            var usersFromList = usersList.GetAllUsers();

            Assert.That(usersFromList.All(u => u != _user), Is.True);
        }

        [Test]
        public void UpdateUserInList()
        {
            var usersList = new ListUserRepository();

            usersList.AddUser(_user);

            var usersFromList = usersList.GetAllUsers();

            var newUser = _user;
            
            newUser.Name = "Semen";

            usersList.UpdateUser(newUser);

            Assert.True(newUser.Equals(usersFromList.First()));

            newUser.Id = 213;
            newUser.Name = "Vasya";
            usersList.DeleteUser(newUser);

            usersList.GetAllUsers();

            Assert.That(usersList.GetUser(213), Is.EqualTo(null));

        }

        [Test]
        public void GetUserFromList()
        {
            var usersList = new ListUserRepository();

            usersList.AddUser(_user);

            var usersFromList = usersList.GetAllUsers().ToArray();

            var userid = usersFromList.First().Id;

            var userWithNecessaryId = usersList.GetUser(userid);


            Assert.True(usersFromList.First().Equals(userWithNecessaryId));
        }
    }
}