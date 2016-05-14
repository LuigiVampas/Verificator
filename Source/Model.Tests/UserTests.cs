using NUnit.Framework;

namespace Model.Tests
{
    [TestFixture]
    internal class UserTests
    {
        [Test]
        public void CreateUserTest()
        {
            var user = new User
            {
                Name = "Andrey",
                Surname = "Sokov",
                Lastname = "Igorevich",
                Login = "cwdlcs",
                Password = "1234567890!LoL1234567890",
                Position = "Engineer"
            };

            Assert.That(user.Name, Is.EqualTo("Andrey"));
            Assert.That(user.Surname, Is.EqualTo("Sokov"));
            Assert.That(user.Lastname, Is.EqualTo("Igorevich"));
            Assert.That(user.Login, Is.EqualTo("cwdlcs"));
            Assert.That(user.Password, Is.EqualTo("1234567890!LoL1234567890"));
            Assert.That(user.Position, Is.EqualTo("Engineer"));
            Assert.That(user.Initials, Is.EqualTo("A.I."));

        }

        [Test]
        public void AreUsersEqualOrUnequalTest()
        {
            var rightUser = new User
            {
                Name = "Andrey",
                Surname = "Sokov",
                Lastname = "Igorevich",
                Login = "cwdlcs",
                Password = "1234567890!LoL1234567890",
                Position = "Engineer"
            };

            var leftUser = new User
            {
                Name = "Andrey",
                Surname = "Sokov",
                Lastname = "Igorevich",
                Login = "cwdlcs",
                Password = "1234567890!LoL1234567890",
                Position = "Engineer"
            };

            var uncorrectUser = new User
            {
                Name = "Andrey",
                Surname = "Sokosdaav",
                Lastname = "Igorevich",
                Login = "cwdlcs",
                Password = "1234567890!LoL1234567890",
                Position = "Engineer"
            };


            Assert.True(leftUser.Equals(rightUser));
            Assert.True(leftUser != uncorrectUser);
        }

        [Test]
        public void UsersHashTest()
        {
            var user = new User();
            var hashCode = user.Id;
            hashCode = (hashCode*397) ^ (user.Login != null ? user.Login.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Password != null ? user.Password.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Surname != null ? user.Surname.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Name != null ? user.Name.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Lastname != null ? user.Lastname.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Initials != null ? user.Initials.GetHashCode() : 0);
            hashCode = (hashCode*397) ^ (user.Position != null ? user.Position.GetHashCode() : 0);

            Assert.That(hashCode, Is.EqualTo(user.GetHashCode()));
        }

        [Test]
        public void UserObjectEqualityTest()
        {
            var user = new User();

            Assert.That(user.Equals(null), Is.False);

            Assert.That(user.Equals(10), Is.False);
        }
    }
}
