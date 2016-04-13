using NUnit.Framework;

namespace Model.Tests
{
    [TestFixture]
    class UserTests
    {
        [Test]
        public void CreateUserTest()
        {
            var user = new User
            {
                Name = "Andrey",
                Surname = "Igorevich",
                Lastname = "Sokov",
                Login = "cwdlcs",
                Password = "1234567890!LoL1234567890",
                Position = "Engineer"
            };

            Assert.That(user.Name, Is.EqualTo("Andrey"));
            Assert.That(user.Surname, Is.EqualTo("Igorevich"));
            Assert.That(user.Lastname, Is.EqualTo("Sokov"));
            Assert.That(user.Login, Is.EqualTo("cwdlcs"));
            Assert.That(user.Password, Is.EqualTo("1234567890!LoL1234567890"));
            Assert.That(user.Position, Is.EqualTo("Engineer"));
            Assert.That(user.Initials, Is.EqualTo("A.I"));

        }
    }
}
